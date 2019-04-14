using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentAssignedTracking
    {
        public int TrackingId { get; set; }
        public int EquipmentId { get; set; }
        public string TrackingType { get; set; }
        public string IntervalType { get; set; }
        public int InitialInterval { get; set; }
        public int RecurringInterval { get; set; }
        public int NotificationPercent { get; set; }
        public string ScheduledComponent { get; set; }
    }
}
