using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserEmailCalendar
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string EmailAddress { get; set; }
        public string CalendarId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ReadOnly { get; set; }
        public bool Sync { get; set; }
        public bool Tasks { get; set; }
        public bool Calls { get; set; }
        public bool Events { get; set; }
    }
}
