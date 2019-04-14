using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class AutomationService
    {
        public int ServiceId { get; set; }
        public string Service { get; set; }
        public string ScheduleType { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? StopTime { get; set; }
        public int Interval { get; set; }
        public DateTime? LastRun { get; set; }
        public DateTime? NextRun { get; set; }
        public bool Active { get; set; }
    }
}
