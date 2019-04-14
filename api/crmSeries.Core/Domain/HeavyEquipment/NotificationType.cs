using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class NotificationType
    {
        public int TypeId { get; set; }
        public string NotificationType1 { get; set; }
        public string DisplayText { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }
}
