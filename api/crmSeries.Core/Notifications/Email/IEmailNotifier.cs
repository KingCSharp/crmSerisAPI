using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace crmSeries.Core.Notifications.Email
{
    public interface IEmailNotifier
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
