using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealAssignedTradeIn
    {
        public int TradeInId { get; set; }
        public int DealId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string SerialNo { get; set; }
        public string StockNo { get; set; }
        public string EquipYear { get; set; }
        public int Hours { get; set; }
        public string Description { get; set; }
        public string Attachment1 { get; set; }
        public string Attachment1SerialNo { get; set; }
        public string Attachment2 { get; set; }
        public string Attachment2SerialNo { get; set; }
        public string Attachment3 { get; set; }
        public string Attachment3SerialNo { get; set; }
        public string Attachment4 { get; set; }
        public string Attachment4SerialNo { get; set; }
        public string Attachment5 { get; set; }
        public string Attachment5SerialNo { get; set; }
        public string PayoffInstitution { get; set; }
        public DateTime? PayoffDate { get; set; }
        public string PayoffDetails { get; set; }
        public string Fob { get; set; }
        public decimal TradeValue { get; set; }
        public decimal Overallowance { get; set; }
        public decimal CustomerOffer { get; set; }
        public bool Deleted { get; set; }
        public decimal Field1 { get; set; }
        public decimal Field2 { get; set; }
        public decimal Field3 { get; set; }
        public decimal Field4 { get; set; }
        public decimal Field5 { get; set; }
        public decimal Field6 { get; set; }
        public decimal Field7 { get; set; }
        public decimal Field8 { get; set; }
        public decimal Field9 { get; set; }
        public decimal Field10 { get; set; }
        public decimal BookValue { get; set; }
        public decimal PayoffAmount { get; set; }
        public decimal ReconditionEstimate { get; set; }
        public string Comments { get; set; }
    }
}
