using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealUserAccessFilter
    {
        public int FilterId { get; set; }
        public int AccessId { get; set; }
        public string RecordType { get; set; }
        public int RecordId { get; set; }
    }
}
