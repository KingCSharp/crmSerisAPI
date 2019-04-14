using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentUchistory
    {
        public int HistoryId { get; set; }
        public int EquipmentId { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        public decimal ActionHours { get; set; }
        public string ActionBy { get; set; }
    }
}
