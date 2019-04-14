using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UdfassignedValue
    {
        public int AssignedId { get; set; }
        public int Udfid { get; set; }
        public string RecordType { get; set; }
        public int RecordId { get; set; }
        public int LookupId { get; set; }
        public string StringValue { get; set; }
        public DateTime? DateValue { get; set; }
        public decimal NumericValue { get; set; }
    }
}
