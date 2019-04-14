using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class LoginPasswordReset
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string ResetCode { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool? Expires { get; set; }
    }
}
