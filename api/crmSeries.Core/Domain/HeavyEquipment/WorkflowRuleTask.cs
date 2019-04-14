using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class WorkflowRuleTask
    {
        public int TaskId { get; set; }
        public int ConditionId { get; set; }
        public string TaskSubject { get; set; }
        public string TaskDueDateTrigger { get; set; }
        public int TaskDueDateInterval { get; set; }
        public string TaskStatus { get; set; }
        public string TaskPriority { get; set; }
        public string TaskDescription { get; set; }
        public bool TaskNotifyAssignedUser { get; set; }
        public bool TaskReminder { get; set; }
        public int TaskReminderDays { get; set; }
        public TimeSpan? TaskReminderTime { get; set; }
    }
}
