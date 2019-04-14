using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class MessageFolder
    {
        public int FolderId { get; set; }
        public int MessageId { get; set; }
        public string NylasId { get; set; }
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
