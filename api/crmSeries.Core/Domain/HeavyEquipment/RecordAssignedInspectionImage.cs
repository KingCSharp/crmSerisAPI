using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RecordAssignedInspectionImage
    {
        public int ImageId { get; set; }
        public int AssignedInspectionId { get; set; }
        public int AssignedItemId { get; set; }
        public string ImagePath { get; set; }
        public string FileName { get; set; }
    }
}
