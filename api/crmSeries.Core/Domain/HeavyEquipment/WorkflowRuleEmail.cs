using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class WorkflowRuleEmail
    {
        public int Id { get; set; }
        public int ConditionId { get; set; }
        public int TemplateId { get; set; }
    }
}
