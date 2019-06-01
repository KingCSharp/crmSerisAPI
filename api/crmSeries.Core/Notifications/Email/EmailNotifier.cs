﻿using crmSeries.Core.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace crmSeries.Core.Notifications.Email
{
    public class EmailNotifier : IEmailNotifier
    {
        private readonly CommonSettings _commonSettings;

        public EmailNotifier(CommonSettings commonSettings)
        {
            _commonSettings = commonSettings;
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(
                    _commonSettings.Smtp.SenderName, 
                    _commonSettings.Smtp.FromAddress));
                message.ToAddresses.ForEach(a => { mimeMessage.To.Add(new MailboxAddress(a.Name, a.Address)); });
                mimeMessage.Subject = message.Subject;
                mimeMessage.Body = new TextPart("html") { Text = message.Body };

                using (var client = new SmtpClient())
                {
                    // Accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(
                        _commonSettings.Smtp.Host, 
                        _commonSettings.Smtp.Port, 
                        _commonSettings.Smtp.UseSsl);
                    await client.AuthenticateAsync(
                        _commonSettings.Smtp.Credentials.UserName, 
                        _commonSettings.Smtp.Credentials.Password);
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
