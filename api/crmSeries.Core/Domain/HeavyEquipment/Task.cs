using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Task
    {
        public int TaskId { get; set; }
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
        public bool Deleted { get; set; }
        public string EventId { get; set; }
        public string CalendarId { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
