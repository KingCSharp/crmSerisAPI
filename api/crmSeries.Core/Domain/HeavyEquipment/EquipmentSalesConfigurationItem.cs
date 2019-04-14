using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSalesConfigurationItem
    {
        public int ItemId { get; set; }
        public int ConfigurationId { get; set; }
        public int ModelItemId { get; set; }
        public int SortOrder { get; set; }
        public int Qty { get; set; }
    }
}
