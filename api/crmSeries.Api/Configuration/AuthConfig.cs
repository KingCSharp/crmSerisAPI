using crmSeries.Api.Requirements;
using crmSeries.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace crmSeries.API.Configuration
{
    public static class AuthConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, ApiKeyRequirementHandler>();
            services.AddAuthorization(authConfig =>
            {
                authConfig.AddPolicy(Constants.Auth.ApiKeyPolicy,
                    policyBuilder => policyBuilder
                        .AddRequirements(new ApiKeyRequirement()));
            });
        }
    }
}