using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace crmSeries.Api.Configuration
{
    public static class IdentityServerConfig
    {
        public const string RoleIdentityResource = "role";
        public const string ApiResourceName = "crmSeriesAPI";
        
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services
                .AddIdentityServer()
                .AddInMemoryClients(GetClients(config))
                .AddInMemoryIdentityResources(GetIdentityResources())
                .AddInMemoryApiResources(GetApiResources(config))
                .AddDeveloperSigningCredential();
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseIdentityServer();
        }

        private static IEnumerable<Client> GetClients(IConfiguration config)
        {
            var clients = config
                .GetSection("Identity:Clients")
                .Get<List<Client>>();

            var clientSecret = new Secret(config["Identity:ClientSecret"].Sha256());
            
            clients.ForEach(x =>
            {
                x.AllowedGrantTypes = GrantTypes.ResourceOwnerPassword;
                x.ClientSecrets = new List<Secret> { clientSecret };
                x.AllowOfflineAccess = true;
                x.RefreshTokenUsage = TokenUsage.OneTimeOnly;
                x.UpdateAccessTokenClaimsOnRefresh = true;
            });

            return clients;
        }

        private static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
            yield return new IdentityResources.Email();

            yield return new IdentityResource
            {
                Name = RoleIdentityResource,
                DisplayName = RoleIdentityResource,
                UserClaims = { RoleIdentityResource }
            };
        }

        private static IEnumerable<ApiResource> GetApiResources(IConfiguration config)
        {
            var scopes = config
                .GetSection("Identity:Scopes")
                .Get<IEnumerable<string>>()
                .Select(x => new Scope(x))
                .ToArray();

            var secret = config["Identity:ApiSecret"];

            yield return new ApiResource
            {
                Name = ApiResourceName,
                DisplayName = ApiResourceName,
                Description = $"{ApiResourceName} Access",
                UserClaims = { RoleIdentityResource },
                ApiSecrets = { new Secret(config["Identity:ApiSecret"].Sha256()) },
                Scopes = scopes
            };
        }
    }
}
