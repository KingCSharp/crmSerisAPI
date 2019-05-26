using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks.Dtos;
using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Features.Tasks.Validator;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Task = crmSeries.Core.Domain.HeavyEquipment.Task;

namespace crmSeries.Core.Features.Tasks
{
    [HeavyEquipmentContext]
    public class EditTaskRequest : EditTaskDto, IRequest
    {
    }

    public class EditTaskHandler : IRequestHandler<EditTaskRequest>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public EditTaskHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }
        
        public Task<Response> HandleAsync(EditTaskRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var taskEntity = request.MapTo<Task>();
            taskEntity.LastModified = DateTime.UtcNow;
            
            _context.Set<Task>().Attach(taskEntity);
            _context.Entry(taskEntity).State = EntityState.Modified;
            _context.SaveChanges();

            return Response.SuccessAsync();
        }

        private bool IsValid(EditTaskRequest request, out Task<Response> errorAsync)
        {
            var relatedEntities = new Dictionary<string, int>
            {
                { request.RelatedRecordType, request.RelatedRecordId },
                { Constants.RelatedRecord.Types.Task, request.TaskId },
                { Constants.RelatedRecord.Types.Contact, request.ContactId },
                { Constants.RelatedRecord.Types.User, request.UserId }
            };

            foreach (var entity in relatedEntities)
            {
                var verifyRelatedRecordRequest = new VerifyRelatedRecordRequest
                {
                    RecordType = entity.Key,
                    RecordTypeId = entity.Value
                };

                var result = _verifyRelatedRecordsHandler.HandleAsync(verifyRelatedRecordRequest).Result;

                if (result.HasErrors)
                {
                    errorAsync = Response.ErrorsAsync(result.Errors);
                    return false;
                }
            }

            errorAsync = null;
            return true;
        }
    }

    public class EditTaskValidator : AbstractValidator<EditTaskRequest>
    {
        public EditTaskValidator()
        {
            RuleFor(x => x.TaskId).GreaterThan(0);
            Include(new BaseTaskDtoValidator());
        }
    }
}