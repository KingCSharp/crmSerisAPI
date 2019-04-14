using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class LoginHistory
    {
        public int HistoryId { get; set; }
        public string Email { get; set; }
        public string Ipaddress { get; set; }
        public DateTime LoginDate { get; set; }
    }
}
