using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class ServiceQuote
    {
        public int QuoteId { get; set; }
        public int CompanyId { get; set; }
        public int ShipToCompanyId { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public int EquipmentId { get; set; }
        public DateTime QuoteDate { get; set; }
        public string QuoteNumber { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string SerialNo { get; set; }
        public string StockNo { get; set; }
        public bool Deleted { get; set; }
    }
}
