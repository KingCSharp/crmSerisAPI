using System;
using System.Linq;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.Admin;
using crmSeries.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Security
{
    public interface IIdentityApiContext
    {
        ApiUser RequestingUser { get; }
    }

    public class HttpIdentityApiContext
    {
        private readonly AdminContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ApiUser _cachedUser;

        public HttpIdentityApiContext(AdminContext dataContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public ApiUser RequestingUser
        {
            get
            {
                if (_cachedUser != null)
                {
                    return _cachedUser;
                }

                var apiKey = _httpContextAccessor.HttpContext.User?.FindFirst(IdentityClaims.ApiKeyClaim)?.Value;
                if (string.IsNullOrEmpty(apiKey))
                {
                    apiKey = _httpContextAccessor.HttpContext.Request.Headers[Constants.Auth.ApiKey];
                }

                var dealer = _context
                    .Set<Dealer>()
                    .AsNoTracking()
                    .SingleOrDefault(x => x.Apikey == apiKey);

                if (dealer == null || string.IsNullOrEmpty(dealer.Dbstring))
                {
                    throw new AuthorizationFailedException(Constants.Auth.UnauthorizedApiKey);
                }

                var apiUser = new ApiUser
                {
                    DealerName = dealer.DealerName,
                    DatabaseConnectionString = dealer.Dbstring,
                    DealerId = dealer.DealerId,
                };

                return _cachedUser = apiUser;
            }
        }
    }

    public class DeferredHttpIdentityApiContext : IIdentityApiContext
    {
        private readonly Lazy<HttpIdentityApiContext> _deferredContext;

        public DeferredHttpIdentityApiContext(Lazy<HttpIdentityApiContext> deferredContext)
        {
            _deferredContext = deferredContext;
        }

        public ApiUser RequestingUser => _deferredContext.Value.RequestingUser;
    }

    public class NullIdentityApiContext : IIdentityApiContext
    {
        public ApiUser RequestingUser => new ApiUser
        {
            DatabaseConnectionString = "foo-bar"
        };
    }
}