using System.Net;

namespace crmSeries.Core.Configuration
{
    public class CommonSettings
    {
        public AdminUserSettings AdminUser { get; } = new AdminUserSettings();

        public AccountSettings Account { get; } = new AccountSettings();

        public SmtpSettings Smtp { get; } = new SmtpSettings();

        public SupportSettings Support { get; } = new SupportSettings();
    }

    public class AdminUserSettings
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LinkObject { get; set; }
    }

    public class AccountSettings
    {
        public int LinkTokenExpirationHours { get; set; }
    }

    public class SmtpSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool UseSsl { get; set; }

        public NetworkCredential Credentials { get; set; }
    }

    public class SupportSettings
    {
    }
}