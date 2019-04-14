using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentPmlabor
    {
        public int LaborId { get; set; }
        public int IntervalId { get; set; }
        public string Description { get; set; }
        public int Hours { get; set; }
        public bool Deleted { get; set; }
    }
}
