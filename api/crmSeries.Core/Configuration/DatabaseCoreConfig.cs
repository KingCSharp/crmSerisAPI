using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace crmSeries.Core.Configuration
{
    public static class DatabaseCoreConfig
    {
        public static void ConfigureBuilder(DbContextOptionsBuilder options, IConfiguration config)
        {
            options.UseSqlServer(config.GetConnectionString(" "));
        }
    }
}
