using System;
using System.Collections.Generic;
using System.Reflection;
using crmSeries.Core.Configuration;
using crmSeries.Core.Data;
using crmSeries.Core.Features.FileStorage;
using crmSeries.Core.Features.Geocoding;
using crmSeries.Core.Logging;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Configuration;
using crmSeries.Core.Security;
using crmSeries.Core.Tests.Mocks;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SimpleInjector;

namespace crmSeries.Core.Tests
{
    [TestFixture]
    public abstract class BaseUnitTest
    {
        private static bool Initialized;

        protected Container Container { get; set; }
        protected IMediator Mediator => Container.GetInstance<IMediator>();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            if (!Initialized)
            {
                AutoMapperConfig.Configure();
                Initialized = true;
            }
        }

        [SetUp]
        public void SetUp()
        {
            Container = new Container();
            var assemblies = new[]
            {
                GetType().GetTypeInfo().Assembly,
                typeof(IRequestHandler<GetGeocodeInfoRequest, GeocodeInfoDto>).GetTypeInfo().Assembly
            };
            Container.ConfigureMediator(assemblies);
            Container.RegisterInstance<ILogger>(new MockLogger());
            Container.Register<IFileStorageProvider, MockFileStorageProvider>();
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
                .EnableSensitiveDataLogging()
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