using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentUccomponent
    {
        public int ComponentId { get; set; }
        public int EquipmentId { get; set; }
        public string Component { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal StartHours { get; set; }
        public bool Replaced { get; set; }
        public DateTime? LastInspectedDate { get; set; }
        public decimal LastInspectedHours { get; set; }
        public decimal LastPercentWorn { get; set; }
        public decimal HoursOfUse { get; set; }
        public decimal HoursPerDay { get; set; }
        public decimal ProjectedMax { get; set; }
        public decimal ProjectedSmu { get; set; }
        public DateTime? ProjectedWearDate { get; set; }
        public decimal LifeRemaining { get; set; }
        public bool Schedule { get; set; }
    }
}
