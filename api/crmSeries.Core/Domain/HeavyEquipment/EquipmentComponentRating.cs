using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentComponentRating
    {
        public int Id { get; set; }
        public string RateType { get; set; }
        public string Rating { get; set; }
        public decimal RatingValue { get; set; }
    }
}
