using crmSeries.Api.Configuration;
using crmSeries.API.Configuration;
using crmSeries.Core.Configuration;
using Exceptionless;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace crmSeries.Api
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IConfiguration _configuration;

        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            _hostingEnvironment = env;
            _configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            SimpleInjectorConfig.ConfigureServices(services, _configuration);
            CorsConfig.ConfigureServices(services);
            AuthorizationConfig.ConfigureServices(services);
            AuthenticationConfig.ConfigureServices(services, _configuration);
            DatabaseConfig.ConfigureServices(services, _configuration);
            IdentityServerConfig.ConfigureServices(services, _configuration);
            MvcConfig.ConfigureServices(services);
            SwaggerConfig.ConfigureServices(services, _configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapperConfig.Configure();
            SimpleInjectorConfig.Configure(app, env, _configuration);
            RewriteConfig.Configure(app, env);
            CorsConfig.Configure(app, _configuration, env);
            AuthenticationConfig.Configure(app);
            MvcConfig.Configure(app, env);
            SwaggerConfig.Configure(app, env, _configuration);
            FluentValidationConfig.Configure();
            IdentityServerConfig.Configure(app);
            app.UseExceptionless(_configuration["CommonSettings:Exceptionless:Key"]);
            app.UseStaticFiles();
        }
    }
}
