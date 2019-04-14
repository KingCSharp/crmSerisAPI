using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class ProspectTimeline
    {
        public int TimelineId { get; set; }
        public int CompanyId { get; set; }
        public string Action { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
