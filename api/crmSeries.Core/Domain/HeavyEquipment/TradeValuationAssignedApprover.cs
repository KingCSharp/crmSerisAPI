using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class TradeValuationAssignedApprover
    {
        public int ApproverId { get; set; }
        public int ValuationId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset? AssignedOn { get; set; }
        public DateTimeOffset? CompleteDate { get; set; }
    }
}
