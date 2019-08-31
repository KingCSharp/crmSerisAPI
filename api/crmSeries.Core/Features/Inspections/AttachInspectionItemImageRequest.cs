using System;
using System.IO;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.FileStorage;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Features.Inspections.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Features.Inspections
{
    [DoNotLog]
    [HeavyEquipmentContext]
    public class AttachInspectionItemImageRequest 
        : RecordAssignedInspectionItemImageDto, IRequest<GetRecordAssignedInspectionItemImageDto>
    {
    }

    public class AttachInspectionItemImageHandler 
        : IRequestHandler<AttachInspectionItemImageRequest, GetRecordAssignedInspectionItemImageDto>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IFileStorageProvider _storageProvider;

        public AttachInspectionItemImageHandler(HeavyEquipmentContext context, IFileStorageProvider storageProvider)
        {
            _context = context;
            _storageProvider = storageProvider;
        }

        public async Task<Response<GetRecordAssignedInspectionItemImageDto>> HandleAsync(AttachInspectionItemImageRequest request)
        {
            if (!await _context.Set<RecordAssignedInspection>().AnyAsync(x => x.AssignedInspectionId == request.AssignedInspectionId))
                return Error.AsResponse<GetRecordAssignedInspectionItemImageDto>(InspectionConstants.ErrorMessages.InspectionRecordNotFound);

            if (!await _context.Set<RecordAssignedInspectionItem>().AnyAsync(x => x.AssignedItemId == request.AssignedItemId))
                return Error.AsResponse<GetRecordAssignedInspectionItemImageDto>(InspectionConstants.ErrorMessages.InspectionRecordItemNotFound);

            var extension = Path.GetExtension(request.FileName)?.ToLower();
            var filename = $"{Path.GetFileNameWithoutExtension(request.FileName)}_{Guid.NewGuid()}{extension}";
            var mimeType = extension.GetMimeType();
            var container = $"inspectionimages-{request.AssignedInspectionId}";
            
            var path = await _storageProvider.StoreFileAsync(container, filename, request.FileStream, mimeType);
            if (string.IsNullOrEmpty(path))
                return Error.AsResponse<GetRecordAssignedInspectionItemImageDto>(InspectionConstants.ErrorMessages.ImageUploadFailed);

            var entry = request.MapTo<RecordAssignedInspectionImage>();
            entry.FileName = request.FileName;
            entry.ImagePath = path;

            _context.Add(entry);

            await _context.SaveChangesAsync();

            return entry.MapTo<GetRecordAssignedInspectionItemImageDto>().AsResponse();
        }
    }

    public class AttachInspectionItemImageValidator : AbstractValidator<AttachInspectionItemImageRequest>
    {
        public AttachInspectionItemImageValidator()
        {
            RuleFor(x => x.AssignedInspectionId)
                .GreaterThan(0);

            RuleFor(x => x.AssignedItemId)
                .GreaterThan(0);

            RuleFor(x => x.FileLength)
                .GreaterThan(0);

            RuleFor(x => x.FileName)
                .NotEmpty();

            RuleFor(x => x.FileStream)
                .NotNull();
        }
    }
}
