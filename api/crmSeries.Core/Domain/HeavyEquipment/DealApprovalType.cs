using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealApprovalType
    {
        public int TypeId { get; set; }
        public string ApprovalType { get; set; }
        public string Description { get; set; }
        public bool ApplyThreshold { get; set; }
    }
}
