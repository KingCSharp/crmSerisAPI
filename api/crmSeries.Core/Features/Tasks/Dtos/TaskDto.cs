using System;
using crmSeries.Core.Domain.HeavyEquipment;

namespace crmSeries.Core.Features.Tasks.Dtos
{
    public class BaseTaskDto
    {
        /// <summary>
        /// The unique identifier for the user this task is assigned to.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The unique identifier for the contact this task is assigned for.
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// The unique identifier for the related record this task is assigned for.
        /// </summary>
        public int RelatedRecordId { get; set; }

        /// <summary>
        /// The type of related record. E.g., Company, Lead, etc.
        /// </summary>
        public string RelatedRecordType { get; set; }

        /// <summary>
        /// The subject for this task.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The comments for this task.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// The start date for this task.
        /// </summary>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// The due date for this task.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// The completion date for this task.
        /// </summary>
        public DateTimeOffset? CompleteDate { get; set; }

        /// <summary>
        /// The current status of this task.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The priority level of this task.
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Denotes if this task has a reminder for it.
        /// </summary>
        public bool Reminder { get; set; }

        /// <summary>
        /// The reminder date for this task.
        /// </summary>
        public DateTimeOffset? ReminderDate { get; set; }

        /// <summary>
        /// The reminder's repeat schedule for this task.
        /// </summary>
        public string ReminderRepeatSchedule { get; set; }

        /// <summary>
        /// The event identifier for this task.
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// The calender identifier for this task.
        /// </summary>
        public string CalendarId { get; set; }
    }

    public class GetTaskDto : BaseTaskDto
    {
        /// <summary>
        /// The unique identifier for this task.
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
        /// The unique identifier for this task.
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
