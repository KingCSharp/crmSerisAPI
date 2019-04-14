using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class WorkflowModule
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string AvailableColumns { get; set; }
        public string ModulePlural { get; set; }
        public string MergedFields { get; set; }
    }
}
