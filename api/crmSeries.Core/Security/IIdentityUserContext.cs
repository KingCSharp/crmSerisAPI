using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
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
        private readonly IIdentityApiContext _apiIdentity;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HeavyEquipmentContext _userContext;
        private IdentityUser _cachedUser;

        public HttpIdentityUserContext(IIdentityApiContext apiIdentity,
            IHttpContextAccessor httpContextAccessor,
            HeavyEquipmentContext userContext)
        {
            _apiIdentity = apiIdentity;
            _httpContextAccessor = httpContextAccessor;
            _userContext = userContext;
        }

        public IdentityUser RequestingUser
        {
            get
            {
                if (_cachedUser != null)
                {
                    return _cachedUser;
                }

                if (string.IsNullOrEmpty(_apiIdentity.RequestingUser?.UserEmail))
                    throw new AuthorizationFailedException(Constants.Auth.NoUser);

                var currentUser = _userContext
                    .Set<User>()
                    .AsNoTracking()
                    .ProjectTo<IdentityUser>()
                    .SingleOrDefault(x => x.Email.ToLower() == _apiIdentity.RequestingUser.UserEmail.ToLower());

                if (currentUser == null)
                    throw new AuthorizationFailedException(Constants.Auth.NoUser);
                
                return _cachedUser = currentUser;
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
            UserId = 0
        };
    }
}
