using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentPminterval
    {
        public int IntervalId { get; set; }
        public int ProfileId { get; set; }
        public int Interval { get; set; }
        public bool Deleted { get; set; }
    }
}
