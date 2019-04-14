using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedAttachment
    {
        public int AttachmentId { get; set; }
        public int DealId { get; set; }
        public int DbattachmentId { get; set; }
        public string Category { get; set; }
        public string Make { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public string OutputDisplay { get; set; }
        public string SerialNo { get; set; }
        public string StockNo { get; set; }
        public string QuoteNo { get; set; }
        public decimal List { get; set; }
        public decimal Cost { get; set; }
        public decimal Freight { get; set; }
        public bool Optional { get; set; }
        public bool UseExchange { get; set; }
        public bool CustomMargin { get; set; }
        public decimal Margin { get; set; }
        public bool Inventory { get; set; }
        public bool Deleted { get; set; }
    }
}
