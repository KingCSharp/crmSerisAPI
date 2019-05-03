using crmSeries.Core.Common;
using crmSeries.Core.Security;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace crmSeries.Core.Notifications.Email
{
    public class EmailNotifier : IEmailNotifier
    {
        private readonly EmailConfig _emailConfig;
        private readonly IIdentityContext _identityContext;

        public EmailNotifier(EmailConfig emailConfig, IIdentityContext identityContext)
        {
            _emailConfig = emailConfig;
            _identityContext = identityContext;
        }

        //TODO: Add CC and BCC support
        public async Task SendEmailAsync(EmailMessage message)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(_emailConfig.SenderName, _emailConfig.FromAddress));
                message.ToAddresses.ForEach(a => { mimeMessage.To.Add(new MailboxAddress(a.Name, a.Address)); });
                mimeMessage.Subject = message.Subject;
                mimeMessage.Body = new TextPart("html") { Text = message.Body };

                using (var client = new SmtpClient())
                {
                    // Accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port, _emailConfig.UseSsl);
                    await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
