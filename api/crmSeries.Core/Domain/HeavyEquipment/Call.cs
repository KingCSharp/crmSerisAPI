using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Call
    {
        public int CallId { get; set; }
        public int UserId { get; set; }
        public int ContactId { get; set; }
        public int RelatedToRecordId { get; set; }
        public string RelatedToRecordType { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public int Reminder { get; set; }
        public bool Deleted { get; set; }
        public string EventId { get; set; }
        public string CalendarId { get; set; }
    }
}
