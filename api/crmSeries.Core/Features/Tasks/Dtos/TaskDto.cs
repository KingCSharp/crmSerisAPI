using System;
using crmSeries.Core.Domain.HeavyEquipment;

namespace crmSeries.Core.Features.Tasks.Dtos
{
    public class BaseTaskDto
    {
        public int UserId { get; set; }

        public int ContactId { get; set; }

        public int RelatedRecordId { get; set; }

        public string RelatedRecordType { get; set; }

        public string Subject { get; set; }

        public string Comments { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTimeOffset? CompleteDate { get; set; }

        public string Status { get; set; }

        public string Priority { get; set; }

        public bool Reminder { get; set; }

        public DateTimeOffset? ReminderDate { get; set; }

        public string ReminderRepeatSchedule { get; set; }

        public string EventId { get; set; }

        public string CalendarId { get; set; }
    }

    public class GetTaskDto : BaseTaskDto
    {
        /// <summary>
        /// The contact identifier.
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// The flag for soft deletion.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// The name of the related record.  This value will be different depending
        /// on what type of related record type is associated with the task.
        /// </summary>
        public string RelatedRecordName { get; set; }

        /// <summary>
        /// The flag for whether this contact is active or not.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The date, if any, that the entity was modified.
        /// </summary>
        public DateTime? LastModified { get; set; }
    }

    public class AddTaskDto : BaseTaskDto
    {
    }

    public class EditTaskDto : BaseTaskDto
    {
        /// <summary>
        /// The task identifier
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// The flag for soft deletion.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// The flag for whether this contact is active or not.
        /// </summary>
        public bool Active { get; set; }
    }
}
