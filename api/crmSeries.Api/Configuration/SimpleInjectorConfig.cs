using System;
using crmSeries.Api.Controllers;
using crmSeries.Core.Configuration;
using crmSeries.Core.Features.FileStorage;
using crmSeries.Core.Logging;
using crmSeries.Core.Mediator;
using crmSeries.Core.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace crmSeries.Api.Configuration
{
    public static class SimpleInjectorConfig
    {
        private static Container _container;

        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            _container = new Container();

            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            _container.ConfigureCore(config);

            _container.Register<HttpIdentityUserContext>(Lifestyle.Scoped);
            _container.Register<IIdentityUserContext>(() =>
            {
                if (_container.IsVerifying)
                {
                    return new NullIdentityUserContext();
                }
                return new DeferredHttpIdentityUserContext(
                    new Lazy<HttpIdentityUserContext>(_container.GetInstance<HttpIdentityUserContext>));
            }, Lifestyle.Scoped);


            _container.Register<HttpIdentityApiContext>(Lifestyle.Scoped);
            _container.Register<IIdentityApiContext>(() =>
            {
                if (_container.IsVerifying)
                {
                    return new NullIdentityApiContext();
                }
                return new DeferredHttpIdentityApiContext(
                    new Lazy<HttpIdentityApiContext>(_container.GetInstance<HttpIdentityApiContext>));
            }, Lifestyle.Scoped);

            _container.RegisterInitializer<BaseApiController>(controller =>
            {
                controller.Mediator = _container.GetInstance<IMediator>();
            });

            services.AddSingleton(_container);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            services.UseSimpleInjectorAspNetRequestScoping(_container);
            services.EnableSimpleInjectorCrossWiring(_container);

            services.AddTransient(provider => _container.GetInstance<ILogger>());
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration config)
        {
            ConfigureFileStorage(config, env);

            _container.CrossWire<IHttpContextAccessor>(app);
            _container.RegisterMvcControllers(app);
            _container.Verify();
        }

        private static void ConfigureFileStorage(IConfiguration config, IHostingEnvironment env)
        {
            switch (config["FileStorage:Provider"]?.ToUpper())
            {
                case "LOCAL":
                    _container.Register<IFileStorageProvider>(() =>
                        new LocalFileStorageProvider(env.WebRootPath, config["FileStorage:PublicUrl"]));
                    break;

                case "AZURE":
                    _container.Register<IFileStorageProvider>(() =>
                        new AzureBlobFileStorageProvider(config["FileStorage:AzureStorageConnectionString"]));
                    break;

                default: break;
            }
        }

        private static Func<T> GetAspNetServiceProvider<T>(IApplicationBuilder app)
        {
            var accessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();

            return () =>
            {
                if (accessor.HttpContext == null)
                    throw new InvalidOperationException("No HttpContext");

                return accessor.HttpContext.RequestServices.GetRequiredService<T>();
            };
        }
    }
}