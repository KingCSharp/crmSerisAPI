using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesWorkOrder
    {
        public int WorkOrderId { get; set; }
        public string Description { get; set; }
        public string OutputDisplay { get; set; }
        public string JobCode { get; set; }
        public decimal Cost { get; set; }
        public int ModelId { get; set; }
        public string Criteria { get; set; }
        public string CriteriaRules { get; set; }
        public bool DefaultGlobal { get; set; }
    }
}
