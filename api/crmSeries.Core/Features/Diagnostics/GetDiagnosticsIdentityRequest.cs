using System.Threading.Tasks;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Security;

namespace crmSeries.Core.Features.Diagnostics
{
    [DoNotValidate]
    public class GetDiagnosticsIdentityRequest : IRequest<IdentityUser>
    {
    }

    public class GetDiagnosticsIdentityRequestHandler : IRequestHandler<GetDiagnosticsIdentityRequest, IdentityUser>
    {
        private readonly IIdentityUserContext _identityContext;

        public GetDiagnosticsIdentityRequestHandler(IIdentityUserContext identityContext)
            => _identityContext = identityContext;

        public Task<Response<IdentityUser>> HandleAsync(GetDiagnosticsIdentityRequest request)
            => _identityContext.RequestingUser.AsResponseAsync();
    }
}
