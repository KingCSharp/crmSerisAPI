using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class WorkflowRule
    {
        public int RuleId { get; set; }
        public string Module { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RuleTrigger { get; set; }
        public string Fields { get; set; }
        public string FieldOperator { get; set; }
        public bool RunOnce { get; set; }
        public bool Active { get; set; }
    }
}
