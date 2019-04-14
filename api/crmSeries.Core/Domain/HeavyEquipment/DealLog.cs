using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealLog
    {
        public int LogId { get; set; }
        public int DealId { get; set; }
        public string RecordType { get; set; }
        public string Action { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset? TimeStamp { get; set; }
        public int RecordId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
