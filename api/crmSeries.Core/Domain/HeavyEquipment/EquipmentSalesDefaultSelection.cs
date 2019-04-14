using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesDefaultSelection
    {
        public int DefaultId { get; set; }
        public int ParentRecordId { get; set; }
        public string ParentRecordType { get; set; }
        public int DefaultRecordId { get; set; }
        public string DefaultRecordType { get; set; }
    }
}
