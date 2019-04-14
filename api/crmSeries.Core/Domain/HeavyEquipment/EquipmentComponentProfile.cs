using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentComponentProfile
    {
        public int ProfileId { get; set; }
        public string Make { get; set; }
        public string ProfileName { get; set; }
        public string SerialRange { get; set; }
        public string EnterBy { get; set; }
        public DateTime EnterDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
