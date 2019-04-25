using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace crmSeries.Core.Notifications.Email
{
    public interface IEmailConfig
    {
        string Host { get; }
        int Port { get; }
        bool UseSsl { get; }
        string FromAddress { get; set; }
        string SenderName { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }

    public class EmailConfig : IEmailConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string FromAddress { get; set; }
        public string SenderName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
