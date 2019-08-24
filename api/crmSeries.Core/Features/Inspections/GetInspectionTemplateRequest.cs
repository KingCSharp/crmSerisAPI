using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Features.Inspections.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Features.Inspections
{
    [HeavyEquipmentContext]
    public class GetInspectionTemplateRequest : IRequest<GetInspectionTemplateDto>
    {
        public GetInspectionTemplateRequest(int inspectionId)
        {
            InspectionId = inspectionId;
        }

        public int InspectionId { get; }
    }

    public class GetInspectionTemplateRequestHandler : IRequestHandler<GetInspectionTemplateRequest, GetInspectionTemplateDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInspectionTemplateRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public async Task<Response<GetInspectionTemplateDto>> HandleAsync(GetInspectionTemplateRequest request)
        {
            var inspection = await _context.Set<Inspection>()
                .ProjectTo<GetInspectionTemplateDto>()
                .SingleOrDefaultAsync(x => x.InspectionId == request.InspectionId);

            if (inspection == null)
                return request.AsErrorResponse(InspectionConstants.ErrorMessages.InspectionNotFound);

            var images = await _context.Set<InspectionImage>()
                .Where(x => x.InspectionId == inspection.InspectionId)
                .ProjectTo<GetInspectionTemplateImageDto>()
                .ToListAsync();

            var groups = await _context.Set<InspectionGroup>()
                .Where(x => x.InspectionId == inspection.InspectionId)
                .ProjectTo<GetInspectionTemplateGroupDto>()
                .ToListAsync();

            var groupIds = groups.Select(x => x.GroupId).ToArray();

            var items = await _context.Set<InspectionItem>()
                .Where(x => groupIds.Contains(x.GroupId))
                .ProjectTo<GetInspectionTemplateItemDto>()
                .GroupBy(x => x.GroupId)
                .ToDictionaryAsync(x => x.Key, x => x.ToList());

            var itemIds = items.SelectMany(x => x.Value).Select(x => x.ItemId).ToArray();

            var responses = await _context.Set<InspectionResponse>()
                .Where(x => itemIds.Contains(x.ItemId))
                .ProjectTo<GetInspectionTemplateResponseDto>()
                .GroupBy(x => x.ItemId)
                .ToDictionaryAsync(x => x.Key, x => x.ToList());

            inspection.Images = images;
            inspection.Groups = groups;
            inspection.Groups.ForEach(group =>
            {
                group.Items = items.ContainsKey(group.GroupId)
                    ? items[group.GroupId]
                    : new List<GetInspectionTemplateItemDto>();

                group.Items.ForEach(item =>
                {
                    item.Responses = responses.ContainsKey(item.ItemId)
                        ? responses[item.ItemId]
                        : new List<GetInspectionTemplateResponseDto>();
                });
            });

            return inspection.AsResponse();
        }
    }

    public class GetInspectionTemplateRequestValidator : AbstractValidator<GetInspectionTemplateRequest>
    {
        public GetInspectionTemplateRequestValidator()
        {
            RuleFor(x => x.InspectionId)
                .GreaterThan(0);
        }
    }
}
