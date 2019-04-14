using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class BranchAssignedUser
    {
        public int AssignedId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public bool DefaultBranch { get; set; }
    }
}
