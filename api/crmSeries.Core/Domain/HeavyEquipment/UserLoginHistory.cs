using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserLoginHistory
    {
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset? LoginDate { get; set; }
    }
}
