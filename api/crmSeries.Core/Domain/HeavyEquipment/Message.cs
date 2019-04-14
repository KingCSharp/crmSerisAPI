using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public string NylasId { get; set; }
        public string AccountId { get; set; }
        public string ThreadId { get; set; }
        public string Subject { get; set; }
        public int Date { get; set; }
        public DateTime? FormattedDate { get; set; }
        public bool Unread { get; set; }
        public bool Starred { get; set; }
        public string Snippet { get; set; }
        public string Body { get; set; }
    }
}
