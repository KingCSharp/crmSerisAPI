using System.Threading.Tasks;
using crmSeries.Core.Security;

namespace crmSeries.API.Tests.Features.Identity.Mocks
{
    public class MockLoginService : ILoginService
    {
        private static readonly LoginDto SuccessResult 
            = new LoginDto { LoginId = 1, DealerId = 2, Email = "test@user", ApiKey = "TestKey" };

        public MockLoginService(bool shouldSucceed = true)
        {
            ShouldSucceed = shouldSucceed;
        }

        public bool ShouldSucceed { get; set; }

        public Task<LoginDto> GetLoginByEmail(string email) => Task.FromResult(ShouldSucceed ? SuccessResult : null);

        public Task<LoginDto> GetLoginByEmail(string email, string password) => Task.FromResult(ShouldSucceed ? SuccessResult : null);

        public Task<LoginDto> GetLoginById(int id) => Task.FromResult(ShouldSucceed ? SuccessResult : null);
    }
}
