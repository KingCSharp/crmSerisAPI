using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public class WorkflowRuleMatch
    {
        public int? ConditionID { get; set; }
    }

    public class WorkflowRuleFieldUpdateMatch
    {
        public int? ConditionID { get; set; }
    }

    public class WorkflowRuleUserAssignment
    {
        public int? UserID { get; set; }
    }
}