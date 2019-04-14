using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentAssignedGet
    {
        public int Getid { get; set; }
        public int EquipmentId { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string PartNo { get; set; }
        public string Size { get; set; }
        public string BoltSize { get; set; }
        public int Qty { get; set; }
        public int BoltQty { get; set; }
        public string Comments { get; set; }
        public bool Deleted { get; set; }
    }
}
