using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class ReportScheduleExecutionHistory
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public DateTimeOffset LastRunDate { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string Distribution { get; set; }
    }
}
