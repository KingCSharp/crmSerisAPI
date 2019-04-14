using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesConfiguration
    {
        public int ConfigurationId { get; set; }
        public int ModelId { get; set; }
        public string Configuration { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public bool DefaultConfig { get; set; }
    }
}
