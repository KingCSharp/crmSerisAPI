using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator.BackgroundJobs;
using crmSeries.Core.Mediator.Configuration;

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

            ConfigureMediator(container, configAssemblies);
        }

        private static CommonSettings ConfigureCommonSettings(IConfiguration config)
        {
            var settings = new CommonSettings();

            settings.AdminUser.Email = config["Common:AdminUser:Email"];
            settings.AdminUser.FirstName = config["Common:AdminUser:FirstName"];
            settings.AdminUser.LastName = config["Common:AdminUser:LastName"];
            settings.AdminUser.LinkObject = config["Common:AdminUser:LinkObject"];

            settings.Account.LinkTokenExpirationHours = config.GetValue<int>("Common:Account:LinkTokenExpirationHours");

            settings.Smtp.Host = config["Common:Smtp:Host"];
            settings.Smtp.Port = config.GetValue<int>("Common:Smtp:Port");
            settings.Smtp.UseSsl = config.GetValue<bool>("Common:Smtp:UseSsl");
            settings.Smtp.Credentials = new NetworkCredential
            (
                config["Common:Smtp:Username"],
                config["Common:Smtp:Password"]
            );

            return settings;
        }

        private static void ConfigureDatabase(Container container, IConfiguration config)
        {
            container.Register<AdminContext>(Lifestyle.Scoped);
            container.Register<HeavyEquipmentContext>(Lifestyle.Scoped);

            //container.Register(() =>
            //{
            //    var options = new DbContextOptionsBuilder();
            //    DatabaseCoreConfig.ConfigureBuilder(options, config);
            //    return options.Options;
            //});

            //container.Register(() => new DataContext(
            //    container.GetInstance<DbContextOptions>()
            //), Lifestyle.Scoped);

            //container.Register<IDataContext>(() => new DataContext(
            //    container.GetInstance<DbContextOptions>()
            //), Lifestyle.Scoped);

            //container.Register<DbContext>(container.GetInstance<DataContext>, Lifestyle.Scoped);
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