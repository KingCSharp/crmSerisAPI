using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealCalculationType
    {
        public int Id { get; set; }
        public string MachineType { get; set; }
        public string CalculationType { get; set; }
        public string MachineFilter { get; set; }
        public string MachineRules { get; set; }
    }
}
