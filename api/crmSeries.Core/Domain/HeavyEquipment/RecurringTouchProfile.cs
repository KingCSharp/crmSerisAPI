using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RecurringTouchProfile
    {
        public int ProfileId { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }
        public string ValueType { get; set; }
        public bool Deleted { get; set; }
    }
}
