using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class ServiceQuoteAssignedPart
    {
        public int AssignedPartId { get; set; }
        public int QuoteId { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal Total { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Margin { get; set; }
        public decimal Discount { get; set; }
    }
}
