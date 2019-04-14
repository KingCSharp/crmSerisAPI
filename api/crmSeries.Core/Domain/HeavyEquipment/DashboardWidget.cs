using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DashboardWidget
    {
        public int WidgetId { get; set; }
        public int GroupId { get; set; }
        public int DataSourceId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string DataFilter { get; set; }
        public string DataRules { get; set; }
        public string WidgetType { get; set; }
        public string WidgetSettings { get; set; }
        public bool UseCache { get; set; }
        public bool ListData { get; set; }
    }
}
