using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class AutomationConfiguration
    {
        public int ConfigId { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
    }
}
