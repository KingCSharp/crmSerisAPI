using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserAssignedRole
    {
        public int AssignedId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool DefaultRole { get; set; }
    }
}
