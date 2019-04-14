using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesFreight
    {
        public int FreightId { get; set; }
        public int ModelId { get; set; }
        public string FreightType { get; set; }
        public int BranchId { get; set; }
        public decimal InboundFreight { get; set; }
        public decimal DeliveryFreight { get; set; }
        public decimal CustomerFreight { get; set; }
        public string Origin { get; set; }
    }
}
