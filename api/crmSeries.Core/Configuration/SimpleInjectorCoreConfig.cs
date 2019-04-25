using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Logging;
using crmSeries.Core.Mediator.BackgroundJobs;
using crmSeries.Core.Mediator.Configuration;
using crmSeries.Core.Security;
using Microsoft.Extensions.DependencyInjection;

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

            var settings = ConfigureCommonSettings(config);
            container.RegisterInstance(settings);
            
            ConfigureDatabase(container, config);
            ConfigureLogging(container);
            ConfigureMediator(container, configAssemblies);
        }

        private static void ConfigureLogging(Container container)
        {
            container.Register<ILogger, CompositeLogger>(Lifestyle.Singleton);
            container.Collection.Register<ILogger>(typeof(ExceptionlessLogger));
        }
   
        private static CommonSettings ConfigureCommonSettings(IConfiguration config)
        {
            var settings = new CommonSettings();

            settings.Smtp.Host = config["Common:Smtp:Host"];
            settings.Smtp.Port = config.GetValue<int>("Common:Smtp:Port");
            settings.Smtp.UseSsl = config.GetValue<bool>("Common:Smtp:UseSsl");
            settings.Smtp.Credentials = new NetworkCredential
            (
                config["Common:Smtp:Username"],
                config["Common:Smtp:Password"]
            );

            settings.Exceptionless.UseExceptionless = config.GetValue<bool>("Common:Exceptionless:Use");
            
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
            var identityContext = container.GetService<IIdentityContext>();

            return identityContext.RequestingUser.DatabaseConnectionString;
        }

        private static void ConfigureMediator(Container container, Assembly[] configAssemblies)
        {
            container.ConfigureMediator(configAssemblies);

            container.Register<IBackgroundJobMediator, BackgroundJobMediator>();
            container.Register(() => new BackgroundJobContext(false));

            container.Register(typeof(IValidator<>), configAssemblies);
        }
    }
}