using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserImpersonationLog
    {
        public int HistoryId { get; set; }
        public int LoggedInUserId { get; set; }
        public int ImpersonationUserId { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
    }
}
