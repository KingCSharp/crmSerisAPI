using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class MessageHeader
    {
        public int HeaderId { get; set; }
        public string MessageId { get; set; }
        public string HeaderName { get; set; }
        public string HeaderValue { get; set; }
    }
}
