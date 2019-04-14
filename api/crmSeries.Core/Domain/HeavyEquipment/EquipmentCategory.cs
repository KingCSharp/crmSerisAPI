using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentCategory
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public bool HasUc { get; set; }
        public bool UctrackingDefault { get; set; }
        public int UcinitialInterval { get; set; }
        public string UcintervalType { get; set; }
        public string UcscheduleComponent { get; set; }
        public bool ComponentTrackingDefault { get; set; }
        public int ComponentInitialInterval { get; set; }
        public string ComponentIntervalType { get; set; }
        public bool InspectionTrackingDefault { get; set; }
        public int InspectionInitialInterval { get; set; }
        public int InspectionRecurringInterval { get; set; }
        public string InspectionIntervalType { get; set; }
        public bool Deleted { get; set; }
    }
}
