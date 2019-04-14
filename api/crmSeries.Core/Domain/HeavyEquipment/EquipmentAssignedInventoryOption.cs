using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentAssignedInventoryOption
    {
        public int OptionId { get; set; }
        public int EquipmentId { get; set; }
        public string Description { get; set; }
        public string SalesCode { get; set; }
        public decimal Cost { get; set; }
        public decimal List { get; set; }
    }
}
