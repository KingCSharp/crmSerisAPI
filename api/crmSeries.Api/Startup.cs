using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using crmSeries.API.Configuration;
using crmSeries.Core.Configuration;
using crmSeries.Api.Configuration;

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
            AuthConfig.ConfigureServices(services);
            DatabaseConfig.ConfigureServices(services, _configuration);
            MvcConfig.ConfigureServices(services);
            SwaggerConfig.ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapperConfig.Configure();
            SimpleInjectorConfig.Configure(app);
            RewriteConfig.Configure(app, env);
            CorsConfig.Configure(app, _configuration, env);
            MvcConfig.Configure(app, env);
            SwaggerConfig.Configure(app, env);
            FluentValidationConfig.Configure();
        }
    }
}
