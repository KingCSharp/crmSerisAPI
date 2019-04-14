using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedApprover
    {
        public int AssignedId { get; set; }
        public int DealId { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset? ApprovalDate { get; set; }
    }
}
