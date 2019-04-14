using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class Login
    {
        public int LoginId { get; set; }
        public int DealerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }
        public string SecurityStamp { get; set; }
        public string HashPassword { get; set; }
        public string Salt { get; set; }
        public bool ChangePassword { get; set; }
        public bool CannotChangePassword { get; set; }
        public bool ForgotPassword { get; set; }
        public string MobileToken { get; set; }
    }
}
