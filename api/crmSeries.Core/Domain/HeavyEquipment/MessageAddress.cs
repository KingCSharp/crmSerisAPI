using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class MessageAddress
    {
        public int AddressId { get; set; }
        public int MessageId { get; set; }
        public string AddressType { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}
