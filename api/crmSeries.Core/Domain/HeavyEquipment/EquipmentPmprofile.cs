using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentPmprofile
    {
        public int ProfileId { get; set; }
        public string Make { get; set; }
        public string ProfileName { get; set; }
        public string SerialRange { get; set; }
        public int InitialServiceInterval { get; set; }
        public int ServiceIntervalStep { get; set; }
        public int ServiceMaxHours { get; set; }
        public bool Deleted { get; set; }
    }
}
