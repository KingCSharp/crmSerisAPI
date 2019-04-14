using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesModelWarranty
    {
        public int AssignedId { get; set; }
        public int WarrantyId { get; set; }
        public int ModelId { get; set; }
        public int Hours { get; set; }
        public int Months { get; set; }
        public decimal Amount { get; set; }
        public string Criteria { get; set; }
        public string CriteriaRules { get; set; }
        public bool DefaultGlobal { get; set; }
    }
}
