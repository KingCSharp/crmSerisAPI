using System;
using System.Linq;
using System.Security.Claims;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.Admin;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace crmSeries.Core.Security
{
    public interface IIdentityUserContext
    {
        IdentityUser RequestingUser { get; }
    }

    public class HttpIdentityUserContext : IIdentityUserContext
    {
        private readonly AdminContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IdentityUser _cachedUser;
        private HeavyEquipmentContext _userContext;

        public HttpIdentityUserContext(AdminContext dataContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public IdentityUser RequestingUser
        {
            get
            {
                if (_cachedUser != null)
                {
                    return _cachedUser;
                }

                var apiKey = _httpContextAccessor.HttpContext.Request.Headers[Constants.Auth.ApiKey];
                var currentUserEmail = _httpContextAccessor.HttpContext.Request.Headers[Constants.Auth.Email].ToString();

                var dealer = _context
                    .Set<Dealer>()
                    .AsNoTracking()
                    .SingleOrDefault(x => x.Apikey == apiKey);

                if (dealer == null || string.IsNullOrEmpty(dealer.Dbstring))
                {
                    throw new AuthorizationFailedException(Constants.Auth.UnauthorizedApiKey);
                }

                _userContext = new HeavyEquipmentContext(
                    new DbContextOptionsBuilder()
                        .UseSqlServer(dealer.Dbstring)
                        .Options
                );

                User currentUser = string.IsNullOrEmpty(currentUserEmail)
                    ? null
                    : _userContext
                        .Set<User>()
                        .AsNoTracking()
                        .SingleOrDefault(x => x.Email.ToLower() == currentUserEmail.ToLower());

                if (currentUser == null)
                    throw new Exception(Constants.Auth.NoUser);

                var apiUser = new IdentityUser
                {
                    CurrentUser = currentUser
                };

                return _cachedUser = apiUser;
            }
        }
    }

    public class DeferredHttpIdentityUserContext : IIdentityUserContext
    {
        private readonly Lazy<HttpIdentityUserContext> _deferredContext;

        public DeferredHttpIdentityUserContext(Lazy<HttpIdentityUserContext> deferredContext)
        {
            _deferredContext = deferredContext;
        }

        public IdentityUser RequestingUser => _deferredContext.Value.RequestingUser;
    }

    public class NullIdentityUserContext : IIdentityUserContext
    {
        public IdentityUser RequestingUser => new IdentityUser
        {
            CurrentUser = null
        };
    }
}
