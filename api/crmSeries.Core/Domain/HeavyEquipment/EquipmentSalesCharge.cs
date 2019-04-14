using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesCharge
    {
        public int ChargeId { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public string ChargeType { get; set; }
        public decimal Amount { get; set; }
        public int ModelId { get; set; }
        public bool AddAsCost { get; set; }
        public string SalePriceField { get; set; }
        public string EditType { get; set; }
        public string Category { get; set; }
        public string Criteria { get; set; }
        public string CriteriaRules { get; set; }
        public bool DefaultGlobal { get; set; }
    }
}
