using System;
using System.Collections.Generic;
using System.Linq;
using crmSeries.Api.Filters;
using crmSeries.API.Filters;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace crmSeries.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            var scope = config
                    .GetSection("Identity:Scopes")
                    .Get<IEnumerable<string>>()
                    .First();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "crmSeries API",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Name = "crmSeries"
                    },
                    Description = "<h2>Description</h2><p>This is the API for the crmSeries platform.</p><br /><h3>Usage</h3><ul><li><strong>Authorization (header)</strong><ul><li><p>The crmSeries API uses OAuth2 to authorize requests.  Credentials will be provided by crmSeries.</p><p>The API will issue access tokens in the form of JWTs, which must be sent in the Authorization header as a \"Bearer token\".</p><p>Your credentials should be kept secure! Do not share them in publicly accessible areas such as GitHub, client-side code, etc.</p></li></ul></li><li><strong>api-key (header)</strong><ul><li><p>Some queries will allow the user to provide an API key in lieu of an access token. This API key will be provided by crmSeries.</p><p>Authentication to the API is performed by having the key in the \"api-key\" header (without the quotes) when sending requests.</p><p>Your API key should be kept secure! Do not share your key in publicly accessible areas such as GitHub, client-side code, etc.</p></li></ul></li><li><strong>email (header)</strong><ul><li><p>Currently, this optional header is used for user-specific queries and is not necessary for any other API calls. This will soon be replaced with identity server.</p></li></ul></li><li><strong>PageSize</strong><ul><li><p>For paged requests, this denotes how many items are returned per page. This is capped at 100 items. Entering a number over 100 will still only return 100 max items per page.</p></li></ul></li></ul>"
                });

                options.IncludeXmlComments(
                    $@"{AppDomain.CurrentDomain.BaseDirectory}\Swagger.XML");

                options.IncludeXmlComments(
                    $@"{AppDomain.CurrentDomain.BaseDirectory}\SwaggerCore.XML");
                
                options.OperationFilter<APIKeyHeaderFilter>();
                options.OperationFilter<HideRouteParams>();
                options.DescribeAllEnumsAsStrings();
                
                options.AddSecurityDefinition("OIDC", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Scopes = new Dictionary<string, string> { { scope, "API Access" } },
                    Flow = "password",
                    TokenUrl = "/connect/token",
                    AuthorizationUrl = "/connect/token"
                });

                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "OIDC", new[] { scope } }
                });
            });
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration config)
        {
            var clientId = config
                .GetSection("Identity:Clients")
                .Get<IEnumerable<Client>>()
                .Select(x => x.ClientId)
                .First();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "docs";
                options.DocExpansion(DocExpansion.None);
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "crmSeries API v1");

                options.OAuthClientId(clientId);
                options.OAuthClientSecret(config["Identity:ClientSecret"]);
            });
        }
    }
}