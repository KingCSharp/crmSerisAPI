using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentPmscheduleNote
    {
        public int NoteId { get; set; }
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public bool Deleted { get; set; }
    }
}
