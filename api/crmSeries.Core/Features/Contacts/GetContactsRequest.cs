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
using crmSeries.Core.Domain.HeavyEquipment;

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

            var contacts =
                (from c in _context.Set<Contact>()
                    join assignedUser in _context.Set<CompanyAssignedUser>() 
                        on c.CompanyId equals assignedUser .CompanyId
                    join company in _context.Set<Company>() 
                        on c.CompanyId equals company.CompanyId
                    where assignedUser.UserId == _identity.RequestingUser.CurrentUser.UserId &&
                          c.Active && !c.Deleted
                    select new
                    {
                        c.ContactId,
                        c.CompanyId,
                        c.FirstName,
                        c.MiddleName,
                        c.LastName,
                        c.NickName,
                        c.Phone,
                        c.Cell,
                        c.Fax,
                        c.Email,
                        c.Title,
                        c.Position,
                        c.Department,
                        c.LastModified,
                        company.CompanyName,
                        company.AccountNo
                    })
                .AsQueryable();

            var count = contacts.Count();

            result.PageCount = count / request.PageInfo.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageInfo.PageNumber;
            result.PageSize = request.PageInfo.PageSize;

            result.Items = contacts.ProjectTo<GetContactDto>()
                .GetPagedData(request.PageInfo)
                .ToList();

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
