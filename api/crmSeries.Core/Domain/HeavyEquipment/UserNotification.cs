using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserNotification
    {
        public int NotificationId { get; set; }
        public int TypeId { get; set; }
        public int UserId { get; set; }
        public int RecordId { get; set; }
        public string RecordType { get; set; }
        public DateTimeOffset? NotificationDate { get; set; }
        public DateTime? ClearDate { get; set; }
        public bool SendEmail { get; set; }
        public DateTime? EmailSentDate { get; set; }
        public bool SendPopup { get; set; }
        public DateTime? PopupSentDate { get; set; }
        public string JobId { get; set; }
    }
}
