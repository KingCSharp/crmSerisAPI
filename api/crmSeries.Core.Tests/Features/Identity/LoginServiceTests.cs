using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.Admin;
using crmSeries.Core.Security;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Identity
{
    [TestFixture]
    public class LoginServiceTests : BaseUnitTest
    {
        private readonly Mocks.MockPasswordService _passwordService = new Mocks.MockPasswordService();
        
        [Test]
        public async Task GetLoginByEmail_WithActiveUser_ReturnsUser()
        {
            // Arrange
            var options = GetAdminContextOptions();

            using (var context = new AdminContext(options))
            {
                var service = new LoginService(context, _passwordService);

                context.Set<Login>().Add(new Login
                {
                    LoginId = 101,
                    DealerId = 2,
                    Email = "test@user",
                    HashPassword = "password",
                    Salt = "word",
                    Active = true
                });

                context.Set<Dealer>().Add(new Dealer
                {
                    DealerId = 2,
                    Apikey = "api-key"
                });
                
                context.SaveChanges();

                // Act
                var result = await service.GetLoginByEmail("test@user");

                // Assert
                Assert.NotNull(result);
                Assert.AreEqual(101, result.LoginId);
                Assert.AreEqual("api-key", result.ApiKey);
            }
        }

        [Test]
        public async Task GetLoginByEmail_WithNoMatchingUser_ReturnsNull()
        {
            // Arrange
            var options = GetAdminContextOptions();

            using (var context = new AdminContext(options))
            {
                var service = new LoginService(context, _passwordService);

                // Act
                var result = await service.GetLoginByEmail("some@user");

                // Assert
                Assert.IsNull(result);
            }
        }

        [Test]
        public async Task GetLoginByEmail_WithInactiveUser_ReturnsNull()
        {
            // Arrange
            var options = GetAdminContextOptions();

            using (var context = new AdminContext(options))
            {
                var service = new LoginService(context, _passwordService);

                context.Set<Login>().Add(new Login
                {
                    LoginId = 101,
                    DealerId = 2,
                    Email = "test@user",
                    HashPassword = "password",
                    Salt = "word",
                    Active = false
                });

                context.SaveChanges();

                // Act
                var result = await service.GetLoginByEmail("test@user");

                // Assert
                Assert.IsNull(result);
            }
        }

        [Test]
        public async Task GetLoginByEmail_WithCorrectPassword_ReturnsUser()
        {
            // Arrange
            var options = GetAdminContextOptions();

            using (var context = new AdminContext(options))
            {
                var service = new LoginService(context, _passwordService);

                context.Set<Login>().Add(new Login
                {
                    LoginId = 101,
                    DealerId = 2,
                    Email = "test@user",
                    HashPassword = "password",
                    Salt = "word",
                    Active = true
                });

                context.Set<Dealer>().Add(new Dealer
                {
                    DealerId = 2,
                    Apikey = "api-key"
                });

                context.SaveChanges();

                // Act
                var result = await service.GetLoginByEmail("test@user", "pass");

                // Assert
                Assert.NotNull(result);
                Assert.AreEqual(101, result.LoginId);
                Assert.AreEqual("api-key", result.ApiKey);
            }
        }

        [Test]
        public async Task GetLoginByEmail_WithIncorrectPassword_ReturnsNull()
        {
            // Arrange
            var options = GetAdminContextOptions();

            using (var context = new AdminContext(options))
            {
                var service = new LoginService(context, _passwordService);

                context.Set<Login>().Add(new Login
                {
                    LoginId = 101,
                    DealerId = 2,
                    Email = "test@user",
                    HashPassword = "incorrect_password",
                    Salt = "word",
                    Active = true
                });

                context.SaveChanges();

                // Act
                var result = await service.GetLoginByEmail("test@user", "pass");

                // Assert
                Assert.IsNull(result);
            }
        }

        [Test]
        public async Task GetLoginById_WithActiveUser_ReturnsUser()
        {
            // Arrange
            var options = GetAdminContextOptions();

            using (var context = new AdminContext(options))
            {
                var service = new LoginService(context, _passwordService);

                context.Set<Login>().Add(new Login
                {
                    LoginId = 101,
                    DealerId = 2,
                    Email = "test@user",
                    HashPassword = "password",
                    Salt = "word",
                    Active = true
                });

                context.Set<Dealer>().Add(new Dealer
                {
                    DealerId = 2,
                    Apikey = "api-key"
                });

                context.SaveChanges();

                // Act
                var result = await service.GetLoginById(101);

                // Assert
                Assert.NotNull(result);
                Assert.AreEqual(101, result.LoginId);
                Assert.AreEqual("api-key", result.ApiKey);
            }
        }

        [Test]
        public async Task GetLoginById_WithNoMatchingUser_ReturnsNull()
        {
            // Arrange
            var options = GetAdminContextOptions();

            using (var context = new AdminContext(options))
            {
                var service = new LoginService(context, _passwordService);

                // Act
                var result = await service.GetLoginById(101);

                // Assert
                Assert.IsNull(result);
            }
        }

        [Test]
        public async Task GetLoginById_WithInactiveUser_ReturnsNull()
        {
            // Arrange
            var options = GetAdminContextOptions();

            using (var context = new AdminContext(options))
            {
                var service = new LoginService(context, _passwordService);

                context.Set<Login>().Add(new Login
                {
                    LoginId = 101,
                    DealerId = 2,
                    Email = "test@user",
                    HashPassword = "password",
                    Salt = "word",
                    Active = false
                });

                context.SaveChanges();

                // Act
                var result = await service.GetLoginById(101);

                // Assert
                Assert.IsNull(result);
            }
        }
    }
}
