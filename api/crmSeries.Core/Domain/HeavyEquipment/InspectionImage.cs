using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class InspectionImage
    {
        public int ImageId { get; set; }
        public int InspectionId { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
    }
}
