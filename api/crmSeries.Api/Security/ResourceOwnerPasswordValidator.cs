using System.Threading.Tasks;
using crmSeries.Core.Security;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace crmSeries.Api.Security
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public const string Grant = "password";

        private readonly ILoginService _loginService;

        public ResourceOwnerPasswordValidator(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var login = await _loginService.GetLoginByEmail(context.UserName, context.Password);

            if (login != null)
            {
                context.Result = new GrantValidationResult(login.LoginId.ToString(), Grant);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
                    "No user found with this email and password combination.");
            }
        }
    }
}
