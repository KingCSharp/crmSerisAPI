using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserDefaultRecurringTouchProfile
    {
        public int AssignedId { get; set; }
        public int UserId { get; set; }
        public int ProfileId { get; set; }
        public bool Override { get; set; }
        public int OverrideTouchesPerYear { get; set; }
    }
}
