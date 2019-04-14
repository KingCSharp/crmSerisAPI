using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentPmschedule
    {
        public int ScheduleId { get; set; }
        public int EquipmentId { get; set; }
        public int CompanyId { get; set; }
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Interval { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public DateTime? CompleteDate { get; set; }
    }
}
