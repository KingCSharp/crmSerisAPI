using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesModelDiscount
    {
        public int AssignedId { get; set; }
        public int DiscountId { get; set; }
        public int ModelId { get; set; }
        public decimal Amount { get; set; }
        public string EditType { get; set; }
        public string Criteria { get; set; }
        public string CriteriaRules { get; set; }
        public bool DefaultGlobal { get; set; }
    }
}
