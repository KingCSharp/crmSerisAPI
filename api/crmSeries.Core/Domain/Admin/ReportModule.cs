using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class ReportModule
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string RuntimeParameters { get; set; }
        public string RelatedModules { get; set; }
        public string AvailableColumns { get; set; }
    }
}
