using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class OpportunityStatus
    {
        public int StatusId { get; set; }
        public string Status { get; set; }
        public bool Deleted { get; set; }
    }
}
