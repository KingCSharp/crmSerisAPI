using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class WorkflowRuleAssignment
    {
        public int AssignmentId { get; set; }
        public string ActionType { get; set; }
        public int ActionId { get; set; }
        public string AssignmentObject { get; set; }
        public int AssignmentObjectId { get; set; }
        public bool PrimaryOnly { get; set; }
    }
}
