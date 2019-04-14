using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class NotePurposeAssignedRole
    {
        public int AssignedId { get; set; }
        public int PurposeId { get; set; }
        public int RoleId { get; set; }
    }
}
