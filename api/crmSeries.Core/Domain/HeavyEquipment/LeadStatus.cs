using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class LeadStatus
    {
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string InternalStatus { get; set; }
        public bool Deleted { get; set; }
        public bool DefaultNew { get; set; }
        public bool DefaultConvert { get; set; }
    }
}
