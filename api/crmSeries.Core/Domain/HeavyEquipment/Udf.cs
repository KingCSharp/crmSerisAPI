using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Udf
    {
        public int Udfid { get; set; }
        public string RecordType { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string FieldName { get; set; }
        public string DisplayText { get; set; }
        public string DataType { get; set; }
        public bool? Editable { get; set; }
        public bool? Visible { get; set; }
        public bool Deleted { get; set; }
    }
}
