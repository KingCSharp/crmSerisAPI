using crmSeries.Core.Security;

namespace crmSeries.Core.Tests.Features.Identity.Mocks
{
    public class MockPasswordService : IPasswordService
    {
        public string CreateHash(string password, string salt)
            => $"{password}{salt}";
    }
}
