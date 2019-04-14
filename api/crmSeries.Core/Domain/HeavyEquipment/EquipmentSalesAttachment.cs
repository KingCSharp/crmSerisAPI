using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesAttachment
    {
        public int AttachmentId { get; set; }
        public int MfgId { get; set; }
        public int ModelId { get; set; }
        public string Category { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public string OutputDisplay { get; set; }
        public decimal List { get; set; }
        public decimal Cost { get; set; }
        public bool UseExchange { get; set; }
        public decimal Freight { get; set; }
        public bool Deleted { get; set; }
    }
}
