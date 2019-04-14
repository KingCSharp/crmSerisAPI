using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RecurringTouchHistory
    {
        public int HistoryId { get; set; }
        public int AssignmentId { get; set; }
        public int UserId { get; set; }
        public string RecordType { get; set; }
        public int RecordId { get; set; }
        public DateTime TouchDate { get; set; }
    }
}
