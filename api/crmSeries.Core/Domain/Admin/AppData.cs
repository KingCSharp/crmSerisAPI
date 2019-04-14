using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class AppData
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string Json { get; set; }
    }
}
