using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesDealsheetField
    {
        public int FieldId { get; set; }
        public string Section { get; set; }
        public string FieldName { get; set; }
        public string Label { get; set; }
        public bool Visible { get; set; }
        public bool Required { get; set; }
    }
}
