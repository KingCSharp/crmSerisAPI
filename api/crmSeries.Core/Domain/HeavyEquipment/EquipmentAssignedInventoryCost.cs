using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentAssignedInventoryCost
    {
        public int CostId { get; set; }
        public int EquipmentId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool IncludeInCalculation { get; set; }
        public bool Nbvdisplay { get; set; }
    }
}
