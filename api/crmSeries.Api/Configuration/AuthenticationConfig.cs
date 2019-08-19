using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace crmSeries.Api.Configuration
{
    public static class AuthenticationConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = config["Identity:Authority"];
                    options.Audience = IdentityServerConfig.ApiResourceName;
                    options.RequireHttpsMetadata = config.GetValue<bool>("Identity:RequireHttps");
                });
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
