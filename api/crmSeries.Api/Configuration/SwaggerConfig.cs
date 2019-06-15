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
                    Description = "<h2>Description</h2><hr/>This is the API for the crmSeries software.<br/><br/><br/><b>Usage</b><ul><li><b>email (header)</b><ul><li>Currently, this optional header is used for user-specific queries and is not necessary for any other API calls. This will soon be replaced with identity server.</li></ul></li><li><b>PageSize</b><ul><li>For paged requests, this denotes how many items are returned per page. This is capped at 100 items. Entering a number over 100 will still only return 100 max items per page.</li></ul></li></ul>"
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