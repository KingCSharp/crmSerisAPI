using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesDiscount
    {
        public int DiscountId { get; set; }
        public int MfgId { get; set; }
        public string ProgramType { get; set; }
        public string Description { get; set; }
        public string DiscountType { get; set; }
        public string Target { get; set; }
        public bool Active { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool OnInvoice { get; set; }
        public bool UseExchange { get; set; }
        public bool? ApplyBaseMachine { get; set; }
        public bool? ApplyOptions { get; set; }
    }
}
