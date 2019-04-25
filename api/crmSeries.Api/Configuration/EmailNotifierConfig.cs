using Microsoft.Extensions.Configuration;
using crmSeries.Core.Notifications.Email;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Api.Configuration
{
    public static class EmailNotifierConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.Configure<EmailConfig>(config.GetSection("Common:Smtp"));
            services.AddSingleton<IEmailNotifier, EmailNotifier>();
        }
    }
}
