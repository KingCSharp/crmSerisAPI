using System.Linq;
using System.Net;
using System.Threading.Tasks;
using crmSeries.Core.Common;
using crmSeries.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace crmSeries.Api.Requirements
{
    public class ApiKeyRequirement : IAuthorizationRequirement
    {
    }

    public class ApiKeyRequirementHandler : AuthorizationHandler<ApiKeyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            SucceedRequirementIfApiKeyPresentAndValid(context, requirement);
            return Task.CompletedTask;
        }

        private void SucceedRequirementIfApiKeyPresentAndValid(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            var apiKeyClaim = context.User?.FindFirst(IdentityClaims.ApiKeyClaim);

            if (apiKeyClaim != null && !string.IsNullOrEmpty(apiKeyClaim.Value))
            {
                context.Succeed(requirement);
                return;
            }

            if (context.Resource is AuthorizationFilterContext authorizationFilterContext)
            {
                var apiKey = authorizationFilterContext.HttpContext.Request.Headers[Constants.Auth.ApiKey].FirstOrDefault();
                if (apiKey != null)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    authorizationFilterContext.Result = new JsonResult("A required header, api-key, is missing.")
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                    context.Succeed(requirement);
                }
            }
        }
    }
}
