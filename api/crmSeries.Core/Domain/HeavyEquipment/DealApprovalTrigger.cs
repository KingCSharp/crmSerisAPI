using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealApprovalTrigger
    {
        public int TriggerId { get; set; }
        public int TypeId { get; set; }
        public string AssignmentTrigger { get; set; }
    }
}
