using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CrmSeriesConnectQuery
    {
        public int QueryId { get; set; }
        public string QueryName { get; set; }
        public bool? Process { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int ProcessInterval { get; set; }
        public string FileName { get; set; }
        public string QuerySql { get; set; }
        public string Columns { get; set; }
        public DateTime? LastRun { get; set; }
        public DateTime? NextRun { get; set; }
        public string Status { get; set; }
    }
}
