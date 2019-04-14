using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserEmailAccessToken
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string AccessToken { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
