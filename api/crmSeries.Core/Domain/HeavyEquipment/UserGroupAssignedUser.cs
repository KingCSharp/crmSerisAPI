using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserGroupAssignedUser
    {
        public int AssignedId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}
