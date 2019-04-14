using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentAssignedPmpart
    {
        public int PartId { get; set; }
        public int EquipmentId { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal List { get; set; }
        public int Qty { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool Deleted { get; set; }
    }
}
