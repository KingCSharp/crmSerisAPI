using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using crmSeries.Core.Logging;
using crmSeries.Core.Security;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace crmSeries.Api.Security
{
    public class ProfileService : IProfileService
    {
        public const string DealerClaim = "dealer";
        public const string ApiKeyClaim = "api_key";

        private readonly ILoginService _loginService;
        private readonly ILogger _logger;

        public ProfileService(ILoginService loginService, ILogger logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var loginId = GetLoginId(context.Subject.GetSubjectId());
            var login = loginId.HasValue ? await _loginService.GetLoginById(loginId.Value) : null;

            if (login != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Subject, login.LoginId.ToString()),
                    new Claim(JwtClaimTypes.Email, login.Email ?? string.Empty),
                    new Claim(DealerClaim, login.DealerId.ToString()),
                    new Claim(ApiKeyClaim, login.ApiKey ?? string.Empty)
                };

                context.IssuedClaims = claims;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var loginId = GetLoginId(context.Subject.GetSubjectId());
            var login = loginId.HasValue ? await _loginService.GetLoginById(loginId.Value) : null;

            context.IsActive = login != null;
        }

        private int? GetLoginId(string subjectId)
        {
            if (int.TryParse(subjectId, out var loginId))
                return loginId;

            _logger.Log($"Invalid SubjectId found in {nameof(ProfileService)}", subjectId);

            return null;
        }
    }
}
