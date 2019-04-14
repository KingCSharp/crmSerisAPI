using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;

namespace crmSeries.Api.Configuration
{
    public class RewriteConfig
    {
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "docs"));
        }
    }
}
