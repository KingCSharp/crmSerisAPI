using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CompanyAssignedRank
    {
        public int AssignedId { get; set; }
        public int CompanyId { get; set; }
        public int RankId { get; set; }
        public int RoleId { get; set; }
    }
}
