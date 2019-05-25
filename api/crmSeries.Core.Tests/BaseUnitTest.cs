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

        [SetUp]
        public void Setup()
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

        public IIdentityUserContext GetUserContextStub(int userId, string email)
        {
            return new StubbedIdentityUserContext(userId, email);
        }
    }

    public class StubbedIdentityUserContext : IIdentityUserContext
    {
        private int userId;
        private string userEmail;

        public StubbedIdentityUserContext(int id, string email)
        {
            userId = id;
            userEmail = email;
        }

        public IdentityUser RequestingUser => new IdentityUser
        {
            CurrentUser = new User
            {
                UserId = userId,
                Email = userEmail
            }
        };
    }
}