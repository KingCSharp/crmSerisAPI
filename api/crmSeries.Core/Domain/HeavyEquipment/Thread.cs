using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Thread
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public string AccountId { get; set; }
        public string Subject { get; set; }
        public bool Unread { get; set; }
        public bool Starred { get; set; }
        public int LastMessageTimestamp { get; set; }
        public int LastMessageReceivedTimestamp { get; set; }
        public int LastMessageSentTimestamp { get; set; }
        public int FirstMessageTimestamp { get; set; }
        public string Snippet { get; set; }
        public int Version { get; set; }
        public bool HasAttachments { get; set; }
    }
}
