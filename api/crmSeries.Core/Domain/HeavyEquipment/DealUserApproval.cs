using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealUserApproval
    {
        public int ApprovalId { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public string RecordSelection { get; set; }
        public bool AdjustThreshold { get; set; }
        public string RecordFilter { get; set; }
        public string RecordRules { get; set; }
        public string ThresholdFilter { get; set; }
        public string ThresholdRules { get; set; }
        public bool Deleted { get; set; }
    }
}
