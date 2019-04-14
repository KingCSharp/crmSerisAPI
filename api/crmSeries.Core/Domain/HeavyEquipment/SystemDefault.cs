using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class SystemDefault
    {
        public int DefaultId { get; set; }
        public string Category { get; set; }
        public string DefaultName { get; set; }
        public string DisplayText { get; set; }
        public string DataType { get; set; }
        public string StringValue { get; set; }
        public decimal NumericValue { get; set; }
        public bool BooleanValue { get; set; }
    }
}
