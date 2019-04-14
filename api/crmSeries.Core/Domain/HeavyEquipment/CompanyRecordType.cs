using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CompanyRecordType
    {
        public int TypeId { get; set; }
        public string RecordType { get; set; }
        public bool Deleted { get; set; }
    }
}
