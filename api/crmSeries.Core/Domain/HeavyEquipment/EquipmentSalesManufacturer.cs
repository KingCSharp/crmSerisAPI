using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesManufacturer
    {
        public int MfgId { get; set; }
        public bool MachineMfg { get; set; }
        public bool AttachmentMfg { get; set; }
        public string Manufacturer { get; set; }
        public decimal StandardFactoryDiscount { get; set; }
        public int SortOrder { get; set; }
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        public bool EditExchange { get; set; }
        public bool ExcludeDiscounts { get; set; }
    }
}
