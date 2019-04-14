using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class AddressLookupValue
    {
        public int LookupId { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string County { get; set; }
    }
}
