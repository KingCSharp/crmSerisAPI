using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using crmSeries.Core.Configuration;
using crmSeries.Core.Data;

namespace crmSeries.Api.Configuration
{
    public static class DatabaseConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            //services.AddDbContext<DataContext>(options => DatabaseCoreConfig.ConfigureBuilder(options, config));
            //services.AddScoped<DbContext>(provider => provider.GetService<DataContext>());
        }

        public static void Configure(IApplicationBuilder app)
        {
        //    using (var scope = app.ApplicationServices.CreateScope())
        //    {
        //        var context = scope.ServiceProvider.GetService<DataContext>();
        //        var container = scope.ServiceProvider.GetService<Container>();
        //    }
        }
    }
}
