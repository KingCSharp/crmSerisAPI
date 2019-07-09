using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class OutputTemplateField
    {
        public int FieldId { get; set; }
        public int TemplateId { get; set; }
        public string TemplateField { get; set; }
        public string FieldType { get; set; }
        public string CrmSeriesField { get; set; }
        public bool Calculation { get; set; }
    }
}
