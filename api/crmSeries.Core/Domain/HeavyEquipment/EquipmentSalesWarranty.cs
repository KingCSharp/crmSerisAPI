using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesWarranty
    {
        public int WarrantyId { get; set; }
        public string Warranty { get; set; }
        public string AmountType { get; set; }
        public bool UseWithQuote { get; set; }
        public string Inclusions { get; set; }
        public string Exclusions { get; set; }
        public bool? Deleted { get; set; }
        public string OutputDisplay { get; set; }
    }
}
