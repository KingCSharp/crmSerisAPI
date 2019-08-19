using crmSeries.Api.Requirements;
using crmSeries.Api.Security;
using crmSeries.Core.Common;
using crmSeries.Core.Security;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace crmSeries.API.Configuration
{
    public static class AuthorizationConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, ApiKeyRequirementHandler>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IPasswordService, PasswordService>();

            services.AddAuthorization(authConfig =>
            {
                authConfig.AddPolicy(Constants.Auth.ApiKeyPolicy,
                    policyBuilder => policyBuilder
                        .AddRequirements(new ApiKeyRequirement())
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
            });
        }
    }
}