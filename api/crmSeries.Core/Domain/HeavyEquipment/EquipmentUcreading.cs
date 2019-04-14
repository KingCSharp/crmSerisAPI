using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentUcreading
    {
        public int ReadingId { get; set; }
        public int EquipmentId { get; set; }
        public int UserId { get; set; }
        public int WearProfileId { get; set; }
        public DateTime ReadingDate { get; set; }
        public decimal ReadingHours { get; set; }
        public string TrackGuards { get; set; }
        public string Impact { get; set; }
        public bool Deleted { get; set; }
    }
}
