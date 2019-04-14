using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RegionAssignedBranch
    {
        public int AssignedId { get; set; }
        public int RegionId { get; set; }
        public int BranchId { get; set; }
    }
}
