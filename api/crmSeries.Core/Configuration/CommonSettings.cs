using System.Net;

namespace crmSeries.Core.Configuration
{
    public class CommonSettings
    {
        public SmtpSettings Smtp { get; } = new SmtpSettings();

        public ExceptionlessSettings Exceptionless { get; } = new ExceptionlessSettings();

        public GeocardioSettings Geocardio { get; } = new GeocardioSettings();

        public string BaseURL { get; set; }
    }

    public class ExceptionlessSettings
    {
        public bool UseExceptionless { get; set; }

        public string Key { get; set; }
    }

    public class GeocardioSettings
    {
        public string Key { get; set; }
    }

    public class SmtpSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool UseSsl { get; set; }

        public string FromAddress { get; set; }

        public string SenderName { get; set; }

        public NetworkCredential Credentials { get; set; }
    }
}