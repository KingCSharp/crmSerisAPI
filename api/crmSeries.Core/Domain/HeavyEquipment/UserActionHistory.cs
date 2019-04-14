using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserActionHistory
    {
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public string RecordType { get; set; }
        public int RecordId { get; set; }
        public string Action { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}
