using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public class WorkflowRuleMatch
    {
        public int ConditionID { get; set; }
    }

    public class WorkflowRuleFieldUpdateMatch
    {
        public int ConditionID { get; set; }
    }

    public class WorkflowRuleUserAssignment
    {
        public int UserID { get; set; }
    }

    public class WorkflowRuleUser
    {
        public int UserID { get; set; }

        public int ContactID { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }
    }
}