using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedDiscount
    {
        public int DiscountId { get; set; }
        public int DealId { get; set; }
        public int DbdiscountId { get; set; }
        public string ProgramType { get; set; }
        public string Description { get; set; }
        public string DiscountType { get; set; }
        public string Target { get; set; }
        public bool OnInvoice { get; set; }
        public bool UseExchange { get; set; }
        public bool ApplyBaseMachine { get; set; }
        public bool ApplyOptions { get; set; }
        public decimal Amount { get; set; }
        public decimal Value { get; set; }
        public bool Deleted { get; set; }
        public string EditType { get; set; }
    }
}
