using System.Threading.Tasks;
using crmSeries.Api.Security;
using IdentityServer4.Validation;
using NUnit.Framework;

namespace crmSeries.API.Tests.Features.Identity
{
    [TestFixture]
    public class ResourceOwnerPasswordValidatorTests
    {
        private readonly Mocks.MockLoginService _loginService = new Mocks.MockLoginService();

        [Test]
        public async Task ValidateAsync_WithGoodCredentials_ShouldValidate()
        {
            // Arrange
            _loginService.ShouldSucceed = true;
            var validator = new ResourceOwnerPasswordValidator(_loginService);
            var context = new ResourceOwnerPasswordValidationContext();

            // Act
            await validator.ValidateAsync(context);

            // Assert
            Assert.NotNull(context.Result);
            Assert.IsTrue(string.IsNullOrEmpty(context.Result.Error));
            Assert.NotNull(context.Result.Subject);
        }

        [Test]
        public async Task ValidateAsync_WithBadCredentials_ShouldNotValidate()
        {
            // Arrange
            _loginService.ShouldSucceed = false;
            var validator = new ResourceOwnerPasswordValidator(_loginService);
            var context = new ResourceOwnerPasswordValidationContext();

            // Act
            await validator.ValidateAsync(context);

            // Assert
            Assert.NotNull(context.Result);
            Assert.IsFalse(string.IsNullOrEmpty(context.Result.Error));
        }
    }
}
