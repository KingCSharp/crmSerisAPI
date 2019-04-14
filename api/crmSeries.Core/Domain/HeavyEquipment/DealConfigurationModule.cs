using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealConfigurationModule
    {
        public int ModuleId { get; set; }
        public string Module { get; set; }
        public string Header { get; set; }
        public bool ForceUpperCase { get; set; }
        public bool MarkupCost { get; set; }
    }
}
