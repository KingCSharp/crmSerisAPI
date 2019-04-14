using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class NotificationServiceConnection
    {
        public int UserId { get; set; }
        public string ConnectionId { get; set; }
        public string LastWrite { get; set; }
    }
}
