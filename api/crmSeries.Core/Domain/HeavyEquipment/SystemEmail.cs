using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class SystemEmail
    {
        public int EmailId { get; set; }
        public string EmailType { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
