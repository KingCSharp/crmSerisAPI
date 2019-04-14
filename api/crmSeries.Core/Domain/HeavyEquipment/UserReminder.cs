using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserReminder
    {
        public int ReminderId { get; set; }
        public int UserId { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool SendReminder { get; set; }
        public int RecordId { get; set; }
        public string RecordType { get; set; }
        public int ContactId { get; set; }
        public int RelatedRecordId { get; set; }
        public string RelatedRecordType { get; set; }
    }
}
