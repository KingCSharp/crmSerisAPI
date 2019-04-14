using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class SupportTicket
    {
        public int TicketId { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public DateTime EnterDate { get; set; }
        public string SourcePage { get; set; }
        public string Exception { get; set; }
        public string Message { get; set; }
        public string AcknowledgedBy { get; set; }
        public DateTime? AcknowledgeDate { get; set; }
        public string ClosedBy { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Comments { get; set; }
    }
}
