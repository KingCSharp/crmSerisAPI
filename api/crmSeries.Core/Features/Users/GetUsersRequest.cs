using crmSeries.Core.Data;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using System.Linq;
using FluentValidation;
using crmSeries.Core.Mediator;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Extensions;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Common;

namespace crmSeries.Core.Features.Contacts
{
    [HeavyEquipmentContext]
    public class GetUsersRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetUserDto>>
    {
        /// <summary>
        /// The user's first name.  Any value entered here will return users whose
        /// first name starts with the value entered in the input box.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name.  Any value entered here will return users whose
        /// last name starts with the value entered in the input box.
        /// </summary>
        public string LastName { get; set; }
    }

    public class GetUsersRequestHandler :
        IRequestHandler<GetUsersRequest, PagedQueryResult<GetUserDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;

        public GetUsersRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<GetUserDto>>> HandleAsync(GetUsersRequest request)
        {
            var result = new PagedQueryResult<GetUserDto>();

            var users =
                (from u in _context.Set<User>()
                 select new
                 {
                     u.UserId,
                     u.FirstName,
                     u.LastName
                 })
                 .OrderBy(x => x.UserId)
                 .AsQueryable();

            if (!string.IsNullOrEmpty(request.FirstName))
                users = users.Where(x => x.FirstName.ToLower().StartsWith(request.FirstName.ToLower()));

            if (!string.IsNullOrEmpty(request.LastName))
                users = users.Where(x => x.LastName.ToLower().StartsWith(request.LastName.ToLower()));

            var count = users.Count();

            result.PageCount = count / request.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            result.Items = users
                .ProjectTo<GetUserDto>()
                .GetPagedData(request)
                .ToList();

            return result.AsResponseAsync();
        }
    }

    public class GetUsersValidator : AbstractValidator<GetUsersRequest>
    {
        public GetUsersValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
