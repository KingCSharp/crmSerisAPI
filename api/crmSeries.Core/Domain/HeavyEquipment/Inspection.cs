using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Inspection
    {
        public int InspectionId { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int DefaultTemplateId { get; set; }
        public bool EquipmentRequired { get; set; }
        public bool TradeValuation { get; set; }
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        public bool IncludeReconditionAmount { get; set; }
    }
}
