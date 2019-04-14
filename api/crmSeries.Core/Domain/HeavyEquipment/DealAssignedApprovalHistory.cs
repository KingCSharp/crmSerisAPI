using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedApprovalHistory
    {
        public int AssignedId { get; set; }
        public int DealId { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public string Action { get; set; }
        public DateTimeOffset? ActionDate { get; set; }
        public string Comments { get; set; }
    }
}
