using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealUdfassignedValue
    {
        public int AssignedId { get; set; }
        public int DealId { get; set; }
        public int Udfid { get; set; }
        public int LookupId { get; set; }
        public string StringValue { get; set; }
        public DateTime? DateValue { get; set; }
        public decimal NumericValue { get; set; }
    }
}
