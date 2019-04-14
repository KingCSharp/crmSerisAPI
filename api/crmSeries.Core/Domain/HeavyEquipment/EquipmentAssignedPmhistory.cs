using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentAssignedPmhistory
    {
        public int HistoryId { get; set; }
        public int EquipmentId { get; set; }
        public int UserId { get; set; }
        public DateTime HistoryDate { get; set; }
        public string Comments { get; set; }
        public DateTimeOffset? Offset { get; set; }
    }
}
