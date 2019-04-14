using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class WorkflowRuleCondition
    {
        public int ConditionId { get; set; }
        public int RuleId { get; set; }
        public string ConditionType { get; set; }
        public string ConditionRules { get; set; }
        public string ConditionSql { get; set; }
    }
}
