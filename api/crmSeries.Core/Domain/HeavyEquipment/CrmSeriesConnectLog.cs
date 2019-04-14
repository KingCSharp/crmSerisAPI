using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CrmSeriesConnectLog
    {
        public int LogId { get; set; }
        public int QueryId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string Status { get; set; }
        public string Msg { get; set; }
    }
}
