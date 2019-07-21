using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CompanySource
    {
        public int SourceId { get; set; }
        public string Source { get; set; }
        public bool DefaultExternal { get; set; }
        public bool Deleted { get; set; }
    }
}
