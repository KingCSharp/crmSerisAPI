using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealUdf
    {
        public int Udfid { get; set; }
        public string FieldName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Location { get; set; }
        public string DisplayText { get; set; }
        public string DataType { get; set; }
        public bool Deleted { get; set; }
        public int SortOrder { get; set; }
    }
}
