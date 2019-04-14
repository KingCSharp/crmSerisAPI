using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EventAssignedContact
    {
        public int AssignedId { get; set; }
        public int EventId { get; set; }
        public int ContactId { get; set; }
    }
}
