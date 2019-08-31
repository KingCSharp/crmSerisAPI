using System;
using System.IO;
using System.Threading.Tasks;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.FileStorage;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;

namespace crmSeries.Core.Features.Inspections
{
    [DoNotLog, DoNotValidate] // TODO: Validator
    public class AttachInspectionItemImageRequest : RecordAssignedInspectionItemImageDto, IRequest<GetRecordAssignedInspectionItemImageDto>
    {
    }

    public class AttachInspectionItemImageHandler : IRequestHandler<AttachInspectionItemImageRequest, GetRecordAssignedInspectionItemImageDto>
    {
        private readonly IFileStorageProvider _storageProvider;

        public AttachInspectionItemImageHandler(IFileStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public async Task<Response<GetRecordAssignedInspectionItemImageDto>> HandleAsync(AttachInspectionItemImageRequest request)
        {
            var extension = Path.GetExtension(request.FileName)?.ToLower();
            var filename = $"{Guid.NewGuid()}{extension}";
            var mimeType = extension.GetMimeType();

            var path = await _storageProvider.StoreFileAsync(
                "container", 
                filename, 
                request.FileStream, 
                mimeType);

            //return path.MapTo<GetRecordAssignedInspectionItemImageDto>().AsResponse();
            return new GetRecordAssignedInspectionItemImageDto().AsResponse();
        }
    }
}
