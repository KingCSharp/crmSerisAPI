using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class ContactPosition
    {
        public int PositionId { get; set; }
        public string Position { get; set; }
        public bool Deleted { get; set; }
    }
}
