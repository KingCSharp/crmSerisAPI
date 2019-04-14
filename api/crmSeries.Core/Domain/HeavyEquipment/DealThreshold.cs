using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealThreshold
    {
        public int ThresholdId { get; set; }
        public string ThresholdType { get; set; }
        public decimal Threshold { get; set; }
        public decimal Target { get; set; }
        public string MachineType { get; set; }
        public string UserFilter { get; set; }
        public string UserRules { get; set; }
        public string MachineFilter { get; set; }
        public string MachineRules { get; set; }
        public bool RequireSubmit { get; set; }
        public bool RequireOther { get; set; }
    }
}
