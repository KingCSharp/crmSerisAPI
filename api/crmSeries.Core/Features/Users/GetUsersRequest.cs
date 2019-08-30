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

namespace crmSeries.Core.Features.Contacts
{
    [HeavyEquipmentContext]
    public class GetUsersRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetUserDto>>
    {
        /// <summary>
        /// Any value entered here will return users whose
        /// first name OR last name starts with the value entered in the input box.
        /// </summary>
        public string Search { get; set; }
    }

    public class GetUsersRequestHandler :
        IRequestHandler<GetUsersRequest, PagedQueryResult<GetUserDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetUsersRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
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

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower();

                users = users.Where(x => x.FirstName.ToLower().StartsWith(search) ||
                    x.LastName.ToLower().StartsWith(search));
            }

            var count = users.Count();

            result.PageCount = (count + request.PageSize - 1) / request.PageSize;
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
