using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserRole
    {
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string InternalRole { get; set; }
        public bool AssignRank { get; set; }
        public bool Deleted { get; set; }
        public int DefaultRecurringTouchProfileId { get; set; }
        public int DefaultRecurringTouchOverride { get; set; }
    }
}
