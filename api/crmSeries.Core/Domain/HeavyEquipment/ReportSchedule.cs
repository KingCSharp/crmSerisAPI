using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class ReportSchedule
    {
        public int ScheduleId { get; set; }
        public int ReportId { get; set; }
        public string Description { get; set; }
        public string Frequency { get; set; }
        public string Format { get; set; }
        public string TimeZone { get; set; }
        public string RunTime { get; set; }
        public string RunDay { get; set; }
        public DateTimeOffset NextRunDate { get; set; }
        public string UserSelection { get; set; }
        public string DateSelection { get; set; }
        public DateTimeOffset ScheduleCreationDate { get; set; }
        public string JobId { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
