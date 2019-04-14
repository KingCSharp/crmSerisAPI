using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentAssignedImage
    {
        public int ImageId { get; set; }
        public int EquipmentId { get; set; }
        public string ImagePath { get; set; }
        public string FileName { get; set; }
        public bool HeaderImage { get; set; }
    }
}
