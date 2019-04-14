using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Event
    {
        public int EventId { get; set; }
        public int HostId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public bool AllDay { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public string Description { get; set; }
        public int RelatedToRecordId { get; set; }
        public string RelatedToRecordType { get; set; }
        public bool Deleted { get; set; }
    }
}
