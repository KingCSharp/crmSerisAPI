using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks.Dtos;
using crmSeries.Core.Features.Tasks.Validator;
using crmSeries.Core.Features.Users.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.Tasks
{
    [HeavyEquipmentContext]
    public class AddTaskRequest : AddTaskDto, IRequest<AddResponse>
    {
    }

    public class AddTaskHandler : IRequestHandler<AddTaskRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public AddTaskHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }

        public Task<Response<AddResponse>> HandleAsync(AddTaskRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var task = request.MapTo<Domain.HeavyEquipment.Task>();
            task.LastModified = DateTime.UtcNow;

            _context.Set<Domain.HeavyEquipment.Task>().Add(task);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = task.TaskId
            }.AsResponseAsync();
        }

        private bool IsValid(BaseTaskDto request, out Task<Response<AddResponse>> errorAsync)
        {
            var relatedEntities = new Dictionary<string, int>
            {
                { request.RelatedRecordType, request.RelatedRecordId },
                { Constants.RelatedRecord.Types.Contact, request.ContactId },
                { Constants.RelatedRecord.Types.User, request.UserId }
            };

            foreach(var entity in relatedEntities)
            {
                var verifyRelatedRecordRequest = new VerifyRelatedRecordRequest
                {
                    RecordType = entity.Key,
                    RecordTypeId = entity.Value                    
                };

                var result = _verifyRelatedRecordsHandler.HandleAsync(verifyRelatedRecordRequest).Result;

                if (result.HasErrors)
                {
                    errorAsync = Response<AddResponse>.ErrorsAsync(result.Errors);
                    return false;
                }
            }

            errorAsync = null;
            return true;
        }
    }

    public class AddTaskValidator : AbstractValidator<AddTaskRequest>
    {
        public AddTaskValidator()
        {
            Include(new BaseTaskDtoValidator());
        }
    }
}