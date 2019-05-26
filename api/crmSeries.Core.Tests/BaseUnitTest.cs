using crmSeries.Core.Configuration;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Security;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace crmSeries.Core.Tests
{
    [TestFixture]
    public abstract class BaseUnitTest
    {
        private static bool Initialized = false;

        [SetUp]
        public void Setup()
        {
            if (!Initialized)
            {
                AutoMapperConfig.Configure();
                Initialized = true;
            }
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