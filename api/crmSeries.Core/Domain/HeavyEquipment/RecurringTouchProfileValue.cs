using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RecurringTouchProfileValue
    {
        public int ValueId { get; set; }
        public int ProfileId { get; set; }
        public int RankId { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public int TouchesPerYear { get; set; }
    }
}
