using System.Linq;
using System.Security.Cryptography.X509Certificates;
using crmSeries.Core.Common;
using crmSeries.Core.Features.Tasks.Dtos;
using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Validation;
using FluentValidation;

namespace crmSeries.Core.Features.Tasks.Validator
{
    public class BaseTaskDtoValidator : AbstractValidator<BaseTaskDto>
    {
        public BaseTaskDtoValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.ContactId).GreaterThan(0);
            RuleFor(x => x.RelatedRecordId).GreaterThan(0);
            RuleFor(x => x.RelatedRecordType).NotEmpty();

            RuleFor(x => x.Subject).NotEmpty();

            RuleFor(x => x.DueDate)
                .SetValidator(new DateTimeDefaultValidator())
                .Unless(x => x.DueDate == null)
                .WithMessage(Constants.ErrorMessages.InvalidDate);

            RuleFor(x => x.Status).Must(BeAValidStatus)
                .When(x => !string.IsNullOrEmpty(x.Status))
                .WithMessage(TasksConstants.ErrorMessages.InvalidStatus);

            RuleFor(x => x.Priority).Must(BeAValidPriority)
                .When(x => !string.IsNullOrEmpty(x.Priority))
                .WithMessage(TasksConstants.ErrorMessages.InvalidPriority);

            RuleFor(x => x.RelatedRecordType).MaximumLength(TasksConstants.MaxRelatedRecordTypeLength);
            RuleFor(x => x.Subject).MaximumLength(TasksConstants.MaxSubjectLength);
            RuleFor(x => x.Status).MaximumLength(TasksConstants.MaxStatusLength);
            RuleFor(x => x.Priority).MaximumLength(TasksConstants.MaxPriorityLength);
            RuleFor(x => x.ReminderRepeatSchedule).MaximumLength(TasksConstants.MaxReminderRepeatScheduleLength);
            RuleFor(x => x.EventId).MaximumLength(TasksConstants.MaxEventIdLength);
            RuleFor(x => x.CalendarId).MaximumLength(TasksConstants.MaxCalendarIdLength);
        }

        private bool BeAValidPriority(string priority)
        {
            return TasksConstants.Priorities.ValidPriorities.Any(x => x == priority);
        }

        private bool BeAValidStatus(string status)
        {
            return TasksConstants.Statuses.ValidStatuses.Any(x => x == status);
        }
    }
}
