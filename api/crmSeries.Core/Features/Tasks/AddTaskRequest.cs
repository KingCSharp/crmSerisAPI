using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks.Dtos;
using crmSeries.Core.Features.Tasks.Validator;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
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
        private readonly IIdentityUserContext _identityContext;

        public AddTaskHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler,
            IIdentityUserContext identityContext)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
            _identityContext = identityContext;
        }

        public Task<Response<AddResponse>> HandleAsync(AddTaskRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var task = request.MapTo<Domain.HeavyEquipment.Task>();
            task.UserId = GetUserId(request.UserId);
            task.LastModified = DateTime.UtcNow;

            _context.Set<Domain.HeavyEquipment.Task>().Add(task);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = task.TaskId
            }.AsResponseAsync();
        }

        private int GetUserId(int userId)
        {
            if (userId != default(int))
                return _identityContext.RequestingUser.UserId;
            else
                return userId;
        }

        private bool IsValid(BaseTaskDto request, out Task<Response<AddResponse>> errorAsync)
        {
            var relatedEntities = new List<(string, int)>
            {
                (request.RelatedRecordType, request.RelatedRecordId),
                (Constants.RelatedRecord.Types.Contact, request.ContactId),
                (Constants.RelatedRecord.Types.User, request.UserId)
            };

            foreach(var (relatedRecordType, relatedRecordTypeId) in relatedEntities)
            {
                var verifyRelatedRecordRequest = new VerifyRelatedRecordRequest
                {
                    RecordType = relatedRecordType,
                    RecordTypeId = relatedRecordTypeId                  
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