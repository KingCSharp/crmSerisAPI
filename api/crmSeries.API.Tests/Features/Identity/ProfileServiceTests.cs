using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using crmSeries.Api.Security;
using IdentityModel;
using IdentityServer4.Models;
using NUnit.Framework;

namespace crmSeries.API.Tests.Features.Identity
{
    [TestFixture]
    public class ProfileServiceTests
    {
        private readonly Mocks.MockLoginService _loginService = new Mocks.MockLoginService();
        private readonly Mocks.MockLogger _logger = new Mocks.MockLogger();
        
        [Test]
        public async Task IsActiveAsync_WithValidLogin_ShouldBeActive()
        {
            // Arrange
            var claims = new[] { new Claim(JwtClaimTypes.Subject, "1") };
            var identity = new ClaimsIdentity(claims, "test");
            _loginService.ShouldSucceed = true;

            var service = new ProfileService(_loginService, _logger);
            var context = new IsActiveContext(new ClaimsPrincipal(identity), new Client(), nameof(ProfileServiceTests));

            // Act
            await service.IsActiveAsync(context);

            // Assert
            Assert.IsTrue(context.IsActive);
        }

        [Test]
        public async Task IsActiveAsync_WithInvalidLogin_ShouldBeInactive()
        {
            // Arrange
            var claims = new[] { new Claim(JwtClaimTypes.Subject, "1") };
            var identity = new ClaimsIdentity(claims, "test");
            _loginService.ShouldSucceed = false;

            var service = new ProfileService(_loginService, _logger);
            var context = new IsActiveContext(new ClaimsPrincipal(identity), new Client(), nameof(ProfileServiceTests));

            // Act
            await service.IsActiveAsync(context);

            // Assert
            Assert.IsFalse(context.IsActive);
        }

        [Test]
        public async Task IsActiveAsync_WithInvalidSubjectId_ShouldLogError()
        {
            // Arrange
            var claims = new[] { new Claim(JwtClaimTypes.Subject, "Invalid") };
            var identity = new ClaimsIdentity(claims, "test");

            var service = new ProfileService(_loginService, _logger);
            var context = new IsActiveContext(new ClaimsPrincipal(identity), new Client(), nameof(ProfileServiceTests));

            // Act
            await service.IsActiveAsync(context);

            // Assert
            Assert.IsNotEmpty(_logger.Messages);
            Assert.IsTrue(_logger.Messages.Any(x => x.Message.StartsWith("Invalid SubjectId found")));
        }

        [Test]
        public async Task GetProfileDataAsync_WithValidLogin_ShouldGetProfileClaims()
        {
            // Arrange
            var claims = new[] { new Claim(JwtClaimTypes.Subject, "1") };
            var identity = new ClaimsIdentity(claims, "test");
            _loginService.ShouldSucceed = true;

            var service = new ProfileService(_loginService, _logger);
            var context = new ProfileDataRequestContext(new ClaimsPrincipal(identity), new Client(), nameof(ProfileServiceTests), new string[0]);

            // Act
            await service.GetProfileDataAsync(context);

            // Assert
            Assert.NotNull(context.IssuedClaims);
            Assert.IsTrue(context.IssuedClaims.Any(x => x.Type == JwtClaimTypes.Subject && x.Value == "1"));
            Assert.IsTrue(context.IssuedClaims.Any(x => x.Type == JwtClaimTypes.Email && x.Value == "test@user"));
            Assert.IsTrue(context.IssuedClaims.Any(x => x.Type == ProfileService.DealerClaim && x.Value == "2"));
        }

        [Test]
        public async Task GetProfileDataAsync_WithInvalidLogin_ShouldNotGetProfileClaims()
        {
            // Arrange
            var claims = new[] { new Claim(JwtClaimTypes.Subject, "1") };
            var identity = new ClaimsIdentity(claims, "test");
            _loginService.ShouldSucceed = false;

            var service = new ProfileService(_loginService, _logger);
            var context = new ProfileDataRequestContext(new ClaimsPrincipal(identity), new Client(), nameof(ProfileServiceTests), new string[0]);

            // Act
            await service.GetProfileDataAsync(context);

            // Assert
            Assert.IsEmpty(context.IssuedClaims);
        }

        [Test]
        public async Task GetProfileDataAsync_WithInvalidSubjectId_ShouldLogError()
        {
            // Arrange
            var claims = new[] { new Claim(JwtClaimTypes.Subject, "Invalid") };
            var identity = new ClaimsIdentity(claims, "test");

            var service = new ProfileService(_loginService, _logger);
            var context = new ProfileDataRequestContext(new ClaimsPrincipal(identity), new Client(), nameof(ProfileServiceTests), new string[0]);

            // Act
            await service.GetProfileDataAsync(context);

            // Assert
            Assert.IsNotEmpty(_logger.Messages);
            Assert.IsTrue(_logger.Messages.Any(x => x.Message.StartsWith("Invalid SubjectId found")));
        }
    }
}
