using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserAlertClearLog
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public string RecordType { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset? ClearDate { get; set; }
    }
}
