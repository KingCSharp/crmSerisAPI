using System.Collections.Generic;
using crmSeries.Core.Data;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using System.Linq;
using FluentValidation;
using crmSeries.Core.Mediator;
using crmSeries.Core.Features.Contacts.Dtos;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Extensions;

namespace crmSeries.Core.Features.Contacts
{
    [HeavyEquipmentContext]
    public class GetContactsRequest : IRequest<PagedQueryResult<GetContactDto>>
    {
        public PagedQueryRequest PageInfo { get; set; }
    }

    public class GetContactsRequestHandler :
        IRequestHandler<GetContactsRequest, PagedQueryResult<GetContactDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;

        public GetContactsRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }
        
        public Task<Response<PagedQueryResult<GetContactDto>>> HandleAsync(GetContactsRequest request)
        {
            var result = new PagedQueryResult<GetContactDto>();

            var contacts = (from c in _context.Contact
                    join assignedUser in _context.CompanyAssignedUser
                        on c.CompanyId equals assignedUser.CompanyId
                    where assignedUser.UserId == _identity.RequestingUser.CurrentUser.UserId && 
                          c.Active && !c.Deleted
                    select c)
                .AsQueryable();

            var count = contacts.Count();

            result.PageCount = count / request.PageInfo.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageInfo.PageNumber;
            result.PageSize = request.PageInfo.PageSize;

            if (count > 0)
            {
                result.Items = contacts.ProjectTo<GetContactDto>().GetPagedData(request.PageInfo)
                    .ToList();
            }
            else
            {
                result.Items = new List<GetContactDto>();
            }

            return result.AsResponseAsync();
        }
    }

    public class GetContactsValidator : AbstractValidator<GetContactsRequest>
    {
        public GetContactsValidator()
        {
            RuleFor(x => x.PageInfo.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageInfo.PageSize)
                .GreaterThan(0);
        }
    }
}
