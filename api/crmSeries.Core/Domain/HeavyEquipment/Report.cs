using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Report
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string SubGroup { get; set; }
        public string ReportType { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReportMetaData { get; set; }
        public string ReportInputModel { get; set; }
        public bool Deleted { get; set; }
    }
}
