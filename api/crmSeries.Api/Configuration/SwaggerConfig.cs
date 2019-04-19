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
                    }
                });

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