using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentAssignedPmservice
    {
        public int ServiceId { get; set; }
        public int EquipmentId { get; set; }
        public string Status { get; set; }
        public int Smuinterval { get; set; }
        public decimal ServiceDueSmu { get; set; }
        public DateTime? ServiceDueDate { get; set; }
        public int ServiceDueInterval { get; set; }
        public decimal ServiceSmu { get; set; }
        public DateTime? ServiceDate { get; set; }
        public string JobNo { get; set; }
        public bool Scheduled { get; set; }
        public int ScheduledUserId { get; set; }
        public int ScheduledVehicleId { get; set; }
        public DateTime? ScheduleStart { get; set; }
        public DateTime? ScheduleEnd { get; set; }
    }
}
