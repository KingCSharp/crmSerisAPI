using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedCharge
    {
        public int ChargeId { get; set; }
        public int DealId { get; set; }
        public int DbchargeId { get; set; }
        public string Target { get; set; }
        public string Description { get; set; }
        public string ChargeType { get; set; }
        public decimal Amount { get; set; }
        public decimal Value { get; set; }
        public bool Deleted { get; set; }
        public bool AddAsCost { get; set; }
        public string SalePriceField { get; set; }
        public string EditType { get; set; }
    }
}
