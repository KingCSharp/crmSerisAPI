using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedRecordLimit
    {
        public int LimitId { get; set; }
        public string RecordType { get; set; }
        public int Limit { get; set; }
    }
}
