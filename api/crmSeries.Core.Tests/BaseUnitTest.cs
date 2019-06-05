using System;
using crmSeries.Core.Configuration;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Security;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace crmSeries.Core.Tests
{
    [TestFixture]
    public abstract class BaseUnitTest
    {
        private static bool Initialized = false;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            if (!Initialized)
            {
                AutoMapperConfig.Configure();
                Initialized = true;
            }
        }

        public DbContextOptions<AdminContext> GetAdminContextOptions()
        {
            string dbName = Guid.NewGuid().ToString();
            return new DbContextOptionsBuilder<AdminContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        public DbContextOptions<HeavyEquipmentContext> GetHeavyEquipmentContextOptions()
        {
            string dbName = Guid.NewGuid().ToString();
            return new DbContextOptionsBuilder<HeavyEquipmentContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        public IIdentityUserContext GetUserContextStub(int userId)
        {
            return new StubbedIdentityUserContext(userId);
        }
    }

    public class StubbedIdentityUserContext : IIdentityUserContext
    {
        private int userId;

        public StubbedIdentityUserContext(int id)
        {
            userId = id;
        }

        public IdentityUser RequestingUser => new IdentityUser
        {
            UserId = userId
        };
    }
}