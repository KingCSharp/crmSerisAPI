using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class LeadUserAccess
    {
        public int AccessId { get; set; }
        public int UserId { get; set; }
        public string RecordSelection { get; set; }
        public string RecordFilter { get; set; }
        public string RecordRules { get; set; }
        public bool Deleted { get; set; }
    }
}
