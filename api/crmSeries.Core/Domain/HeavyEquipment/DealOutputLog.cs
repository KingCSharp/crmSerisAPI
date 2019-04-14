using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealOutputLog
    {
        public int AssignedId { get; set; }
        public int DocumentId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTimeOffset? TimeStamp { get; set; }
    }
}
