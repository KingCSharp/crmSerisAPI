using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealDefault
    {
        public int DefaultId { get; set; }
        public string DefaultName { get; set; }
        public string DisplayText { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }
    }
}
