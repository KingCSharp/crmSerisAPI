using System.Collections.Generic;
using System.Net;
using System.Reflection;
using crmSeries.Core.Data;
using crmSeries.Core.Features.DocuSign.Utility;
using crmSeries.Core.Logging;
using crmSeries.Core.Mediator.BackgroundJobs;
using crmSeries.Core.Mediator.Configuration;
using crmSeries.Core.Notifications.Email;
using crmSeries.Core.Security;
using DocuSign.eSign.Client;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace crmSeries.Core.Configuration
{
    public static class SimpleInjectorCoreConfig
    {
        public static void ConfigureCore(this Container container,
            IConfiguration config,
            params Assembly[] additionalAssemblies)
        {
            var assemblyList = new List<Assembly>(1 + additionalAssemblies.Length)
            {
                typeof(SimpleInjectorCoreConfig).GetTypeInfo().Assembly
            };

            assemblyList.AddRange(additionalAssemblies);

            var configAssemblies = assemblyList.ToArray();

            container.Register(() => config);

            container.Register<IEmailNotifier, EmailNotifier>();

            var settings = ConfigureCommonSettings(container, config);
            container.RegisterInstance(settings);
            
            ConfigureDatabase(container, config);
            ConfigureLogging(container);
            ConfigureMediator(container, configAssemblies);
            ConfigureDocuSign(container, config);
        }

        private static void ConfigureLogging(Container container)
        {
            container.Register<ILogger, CompositeLogger>(Lifestyle.Singleton);
            container.Collection.Register<ILogger>(typeof(ExceptionlessLogger));
        }
   
        private static CommonSettings ConfigureCommonSettings(Container container, IConfiguration config)
        {
            var settings = new CommonSettings();

            settings.Smtp.Host = config["CommonSettings:Smtp:Host"];
            settings.Smtp.Port = config.GetValue<int>("CommonSettings:Smtp:Port");
            settings.Smtp.UseSsl = config.GetValue<bool>("CommonSettings:Smtp:UseSsl");
            settings.Smtp.SenderName = config["CommonSettings:Smtp:SenderName"];
            settings.Smtp.FromAddress = config["CommonSettings:Smtp:FromAddress"];
            settings.Smtp.Credentials = new NetworkCredential
            (
                config["CommonSettings:Smtp:Username"],
                config["CommonSettings:Smtp:Password"]
            );

            settings.Exceptionless.UseExceptionless = config.GetValue<bool>("CommonSettings:Exceptionless:Use");
            settings.Geocardio.Key = config.GetValue<string>("CommonSettings:Geocardio:Key");

            settings.BaseURL = config.GetValue<string>("CommonSettings:Server:BaseURL");

            return settings;
        }

        private static void ConfigureDatabase(Container container, IConfiguration config)
        {
            container.Register(() => new HeavyEquipmentContext(
                new DbContextOptionsBuilder()
                    .UseSqlServer(GetConnectionString(container))
                    .Options
            ), Lifestyle.Scoped);

            container.Register(() => new AdminContext(
                new DbContextOptionsBuilder()
                .UseSqlServer(config.GetConnectionString("AdminContext"))
                    .Options
            ), Lifestyle.Scoped);
        }

        private static string GetConnectionString(Container container)
        {
            var identityContext = container.GetService<IIdentityApiContext>();

            return identityContext.RequestingUser.DatabaseConnectionString;
        }

        private static void ConfigureMediator(Container container, Assembly[] configAssemblies)
        {
            container.ConfigureMediator(configAssemblies);

            container.Register<IBackgroundJobMediator, BackgroundJobMediator>();
            container.Register(() => new BackgroundJobContext(false));

            container.Register(typeof(IValidator<>), configAssemblies);
        }

        private static void ConfigureDocuSign(Container container, IConfiguration config)
        {
            var client = new ApiClient();
            client.SetOAuthBasePath(config["DocuSign:AuthServer"]);

            container.Register<IDocuSignClient, DocuSignClient>();
            container.RegisterInstance(new DocuSignContext());
            container.RegisterInstance(new DocuSignClientAccessor() { Client = client });
        }
    }
}