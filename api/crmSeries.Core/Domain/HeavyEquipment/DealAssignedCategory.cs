using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedCategory
    {
        public int AssignedId { get; set; }
        public int DealId { get; set; }
        public int CategoryId { get; set; }
    }
}
