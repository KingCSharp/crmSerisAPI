using System.Collections.Generic;

namespace crmSeries.Core.Notifications.Email
{
    public class EmailAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class EmailAttachment
    {
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public string MediaSubType { get; set; }
        public string Content { get; set; }
    }

    public class EmailMessage
    {
        public EmailAddress FromAddress { get; set; }
        public List<EmailAddress> ToAddresses { get; set; } = new List<EmailAddress>();
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
    }
}
