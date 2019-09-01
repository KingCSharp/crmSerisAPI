using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Features.Inspections.Utility;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Features.Inspections
{
    [HeavyEquipmentContext]
    public class SaveInspectionRecordRequest : IRequest<GetRecordAssignedInspectionDto>
    {
        public SaveInspectionRecordRequest(RecordAssignedInspectionDto inspection)
        {
            Inspection = new SaveInspectionDto
            {
                AssignedInspectionId = null,
                Inspection = inspection
            };
        }

        public SaveInspectionRecordRequest(int assignedInspectionId, RecordAssignedInspectionDto inspection)
        {
            Inspection = new SaveInspectionDto
            {
                AssignedInspectionId = assignedInspectionId,
                Inspection = inspection
            };
        }
    
        public SaveInspectionDto Inspection { get; }
    }

    public class SaveInspectionRecordHandler : IRequestHandler<SaveInspectionRecordRequest, GetRecordAssignedInspectionDto>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public SaveInspectionRecordHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }

        public async Task<Response<GetRecordAssignedInspectionDto>> HandleAsync(SaveInspectionRecordRequest request)
        {
            var verifyResponse = await VerifyRelatedRecords(request.Inspection.Inspection);
            if (verifyResponse.HasErrors)
                return verifyResponse.CopyErrorsTo(new Response<GetRecordAssignedInspectionDto>());

            var deleteResponse = await DeleteExistingInspection(request.Inspection.AssignedInspectionId);
            if (deleteResponse.HasErrors)
                return deleteResponse.CopyErrorsTo(new Response<GetRecordAssignedInspectionDto>());

            var inspection = await CreateInspection(request.Inspection.Inspection);
            var groups = await CreateGroups(request.Inspection.Inspection.Groups, inspection.AssignedInspectionId);
            var items = await CreateItems(request.Inspection.Inspection.Groups, groups);
            var responses = await CreateItemResponses(request.Inspection.Inspection.Groups.SelectMany(x => x.Items), items);
            
            return inspection.MapTo<GetRecordAssignedInspectionDto>().AsResponse();
        }

        private Task<Response> VerifyRelatedRecords(RecordAssignedInspectionDto inspectionDto)
        {
            var verifyRequest = new VerifyRelatedRecordRequest
            {
                RecordType = inspectionDto.RecordType,
                RecordTypeId = inspectionDto.RecordId
            };

            return _verifyRelatedRecordsHandler.HandleAsync(verifyRequest);
        }

        private async Task<Response> DeleteExistingInspection(int? assignedInspectionId)
        {
            if (!assignedInspectionId.HasValue)
                return Response.Success();

            var existingInspection = await _context.Set<RecordAssignedInspection>()
                .SingleOrDefaultAsync(x => x.AssignedInspectionId == assignedInspectionId.Value);

            if (existingInspection == null)
                return Error.AsResponse(InspectionConstants.ErrorMessages.InspectionRecordNotFound);

            existingInspection.Deleted = true;
            return Response.Success();
        }

        private async Task<RecordAssignedInspection> CreateInspection(RecordAssignedInspectionDto inspectionDto)
        {
            var inspection = inspectionDto.MapTo<RecordAssignedInspection>();

            _context.Add(inspection);
            await _context.SaveChangesAsync();

            return inspection;
        }

        private async Task<List<RecordAssignedInspectionGroup>> CreateGroups(IEnumerable<RecordAssignedInspectionGroupDto> groupDtos, int assignedInspectionId)
        {
            var groups = groupDtos.MapTo<List<RecordAssignedInspectionGroup>>();
            groups.ForEach(x => x.AssignedInspectionId = assignedInspectionId);

            _context.AddRange(groups);
            await _context.SaveChangesAsync();

            return groups;
        }

        private async Task<List<RecordAssignedInspectionItem>> CreateItems(IEnumerable<RecordAssignedInspectionGroupDto> groupDtos, 
            IEnumerable<RecordAssignedInspectionGroup> groups)
        {
            var items = groupDtos
                .Zip(groups, (dto, group) =>
                {
                    var groupItems = dto.Items.MapTo<List<RecordAssignedInspectionItem>>();
                    groupItems.ForEach(x => x.AssignedGroupId = group.AssignedGroupId);
                    return groupItems;
                })
                .SelectMany(x => x)
                .ToList();

            _context.AddRange(items);
            await _context.SaveChangesAsync();

            return items;
        }

        private async Task<List<RecordAssignedInspectionItemResponse>> CreateItemResponses(IEnumerable<RecordAssignedInspectionItemDto> itemDtos,
            IEnumerable<RecordAssignedInspectionItem> items)
        {
            var responses = itemDtos
                .Zip(items, (dto, item) =>
                {
                    var itemResponses = dto.Responses.MapTo<List<RecordAssignedInspectionItemResponse>>();
                    itemResponses.ForEach(x => x.AssignedItemId = item.AssignedItemId);
                    return itemResponses;
                })
                .SelectMany(x => x)
                .ToList();

            _context.AddRange(responses);
            await _context.SaveChangesAsync();

            return responses;
        }
    }

    public class SaveInspectionRecordValidator : AbstractValidator<SaveInspectionRecordRequest>
    {
        public SaveInspectionRecordValidator(IValidator<RecordAssignedInspectionDto> inspectionValidator)
        {
            RuleFor(x => x.Inspection)
                .NotNull();

            When(x => x.Inspection != null, () =>
            {
                When(x => x.Inspection.AssignedInspectionId.HasValue, 
                    () => RuleFor(x => x.Inspection.AssignedInspectionId)
                        .GreaterThan(0)
                        .WithName(nameof(SaveInspectionDto.AssignedInspectionId)));

                RuleFor(x => x.Inspection.Inspection)
                    .NotNull()
                    .WithName(nameof(SaveInspectionDto.Inspection))
                    ;//.SetValidator(inspectionValidator);  TODO: InspectionDto validator
            });
        }
    }
}
