using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserSearchFilter
    {
        public int FilterId { get; set; }
        public int UserId { get; set; }
        public string RecordType { get; set; }
        public string Description { get; set; }
        public bool EnableDateRange { get; set; }
        public string Filter { get; set; }
        public string Rules { get; set; }
        public bool DefaultFilter { get; set; }
        public string Scope { get; set; }
    }
}
