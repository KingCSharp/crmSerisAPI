using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EventAssignedReminder
    {
        public int ReminderId { get; set; }
        public int EventId { get; set; }
        public string Description { get; set; }
        public int MinutesBefore { get; set; }
    }
}
