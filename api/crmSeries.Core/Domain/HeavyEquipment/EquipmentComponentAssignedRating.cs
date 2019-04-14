using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentComponentAssignedRating
    {
        public int AssignedId { get; set; }
        public int ComponentId { get; set; }
        public int RatingId { get; set; }

        public EquipmentComponent Component { get; set; }
    }
}
