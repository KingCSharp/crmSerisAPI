using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesRelatedRecordSelection
    {
        public int RelatedId { get; set; }
        public string ParentRecordType { get; set; }
        public int ParentRecordId { get; set; }
        public string ChildRecordType { get; set; }
        public int ChildRecordId { get; set; }
    }
}
