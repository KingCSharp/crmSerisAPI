using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RecurringTouchProfileAction
    {
        public int ActionId { get; set; }
        public int ProfileId { get; set; }
        public string Action { get; set; }
        public bool Enabled { get; set; }
        public string Filter { get; set; }
        public string Rules { get; set; }
    }
}
