using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserAssignedRight
    {
        public int AssignedId { get; set; }
        public int UserId { get; set; }
        public int RightId { get; set; }
    }
}
