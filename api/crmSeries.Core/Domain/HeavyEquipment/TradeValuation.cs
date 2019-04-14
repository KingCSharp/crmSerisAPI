using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class TradeValuation
    {
        public int ValuationId { get; set; }
        public int UserId { get; set; }
        public decimal TradeValue { get; set; }
        public decimal CustomerOffer { get; set; }
        public string Comments { get; set; }
        public DateTimeOffset? CreateDate { get; set; }
        public int CompleteBy { get; set; }
        public DateTimeOffset? CompleteDate { get; set; }
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
        public decimal Overallowance { get; set; }
        public string Status { get; set; }
        public decimal ReconditionEstimate { get; set; }
        public int RecordId { get; set; }
        public string RecordType { get; set; }
    }
}
