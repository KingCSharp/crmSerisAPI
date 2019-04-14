using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesModelItemRelatedComponent
    {
        public int AssignedId { get; set; }
        public int ItemId { get; set; }
        public string RelatedType { get; set; }
        public int RelatedId { get; set; }
    }
}
