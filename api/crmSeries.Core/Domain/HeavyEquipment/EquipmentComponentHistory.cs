using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentComponentHistory
    {
        public int HistoryId { get; set; }
        public int AssignedComponentId { get; set; }
        public string Action { get; set; }
        public int ReplacementComponentId { get; set; }
        public DateTime ActionDate { get; set; }
        public decimal ActionHours { get; set; }
        public string ActionBy { get; set; }
    }
}
