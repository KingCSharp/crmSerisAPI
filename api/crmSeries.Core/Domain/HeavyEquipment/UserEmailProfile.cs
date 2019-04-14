using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserEmailProfile
    {
        public int ProfileId { get; set; }
        public string AccessToken { get; set; }
        public string AccountId { get; set; }
        public string EmailAddress { get; set; }
        public string Provider { get; set; }
        public string TokenType { get; set; }
        public bool? Active { get; set; }
        public DateTimeOffset? DateConnected { get; set; }
        public DateTimeOffset? LastSync { get; set; }
        public DateTimeOffset? DateDisconnected { get; set; }
        public string Status { get; set; }
    }
}
