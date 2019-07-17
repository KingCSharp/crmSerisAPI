using System;
using System.Collections.Generic;
using crmSeries.Core.Configuration;
using crmSeries.Core.Data;
using crmSeries.Core.Security;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace crmSeries.Core.Tests
{
    [TestFixture]
    public abstract class BaseUnitTest
    {
        private static bool Initialized;

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
            var dbName = Guid.NewGuid().ToString();
            return new DbContextOptionsBuilder<AdminContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        public DbContextOptions<HeavyEquipmentContext> GetHeavyEquipmentContextOptions()
        {
            var dbName = Guid.NewGuid().ToString();
            return new DbContextOptionsBuilder<HeavyEquipmentContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        public IIdentityUserContext GetUserContextStub(int userId)
        {
            return new StubbedIdentityUserContext(userId);
        }

        protected void CreateBuilderPersistMethods<T>(DbContext dbContext) where T : class
        {
            BuilderSetup.SetCreatePersistenceMethod<T>(x =>
            {
                dbContext.Set<T>().Add(x);
                dbContext.SaveChanges();
            });

            BuilderSetup.SetCreatePersistenceMethod<IList<T>>(x =>
            {
                dbContext.Set<T>().AddRange(x);
                dbContext.SaveChanges();
            });
        }
    }

    public class StubbedIdentityUserContext : IIdentityUserContext
    {
        private readonly int userId;

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