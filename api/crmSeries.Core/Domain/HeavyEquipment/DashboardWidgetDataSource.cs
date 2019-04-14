using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DashboardWidgetDataSource
    {
        public int DataSourceId { get; set; }
        public string DataSourceName { get; set; }
        public bool Dashboard { get; set; }
        public bool CompanyForm { get; set; }
        public bool EquipmentForm { get; set; }
    }
}
