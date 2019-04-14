using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesModel
    {
        public int ModelId { get; set; }
        public int MfgId { get; set; }
        public int CategoryId { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public bool Managed { get; set; }
        public int Weight { get; set; }
        public int Horsepower { get; set; }
        public string FactoryDiscountType { get; set; }
        public decimal FactoryDiscount { get; set; }
        public bool Deleted { get; set; }
        public bool ConditionalOptions { get; set; }
    }
}
