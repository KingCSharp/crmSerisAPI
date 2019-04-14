using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CompanyAssignedUser
    {
        public int AssignedId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int RoleId { get; set; }
        public bool PrimaryRep { get; set; }
    }
}
