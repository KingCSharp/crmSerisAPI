using System;
using crmSeries.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using crmSeries.API.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace crmSeries.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
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
                    Description = "<h2>Description</h2><p>This is the API for the crmSeries platform.</p><br /><h3>Usage</h3><ul><li><b>api-key (header)</b><ul><li><p>The crmSeries API uses an API key to authenticate requests. This API key will be provided by crmSeries.</p><p>Your API key gives full access to all requests, so be sure to keep it secure! Do not share your key in publicly accessible areas such as GitHub, client-side code, etc.</p><p>Authentication to the API is performed by having the value in your header when sending requests. The API key’s parameter name is \"api - key\" without the quotes.</p></li></ul><li><b>email (header)</b><ul><li><p>Currently, this optional header is used for user-specific queries and is not necessary for any other API calls. This will soon be replaced with identity server.</p></li></ul></li><li><b>PageSize</b><ul><li><p>For paged requests, this denotes how many items are returned per page. This is capped at 100 items. Entering a number over 100 will still only return 100 max items per page.</p></li></ul></li></ul>"
                });

                options.IncludeXmlComments(
                    $@"{AppDomain.CurrentDomain.BaseDirectory}\Swagger.XML");

                options.IncludeXmlComments(
                    $@"{AppDomain.CurrentDomain.BaseDirectory}\SwaggerCore.XML");
                
                options.OperationFilter<APIKeyHeaderFilter>();
                options.OperationFilter<HideRouteParams>();
                options.DescribeAllEnumsAsStrings();
            });
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "docs";
                options.DocExpansion(DocExpansion.None);
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "crmSeries API v1");
            });
        }
    }
}