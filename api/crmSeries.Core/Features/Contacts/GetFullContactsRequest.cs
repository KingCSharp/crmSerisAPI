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
using crmSeries.Core.Features.Tasks.Dtos;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Features.RelatedRecords;

namespace crmSeries.Core.Features.Contacts
{
    [HeavyEquipmentContext]
    public class GetFullContactsRequest : IRequest<PagedQueryResult<GetFullContactDto>>
    {
        public PagedQueryRequest PageInfo { get; set; }
    }

    public class GetFullContactsHandler :
        IRequestHandler<GetFullContactsRequest, PagedQueryResult<GetFullContactDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;

        public GetFullContactsHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<GetFullContactDto>>> HandleAsync(GetFullContactsRequest request)
        {
            var result = new PagedQueryResult<GetFullContactDto>();

            var contacts =
                (from c in _context.Set<Contact>()
                 join assignedUser in _context.Set<CompanyAssignedUser>()
                     on c.CompanyId equals assignedUser.CompanyId
                 join company in _context.Set<Company>()
                     on c.CompanyId equals company.CompanyId
                 where assignedUser.UserId == _identity.RequestingUser.UserId &&
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
                     c.Active,
                     c.LastModified,
                     company.CompanyName,
                     company.AccountNo
                 })
                 .GroupJoin(
                    _context.Task,
                    contact => contact.ContactId,
                    task => task.ContactId,
                    (x, y) => new
                    {
                        Details = x,
                        Tasks = y.Where(t => !t.Deleted)
                    }
                )
                .GroupJoin(
                    _context.Note,
                    contact => contact.Details.ContactId,
                    notes => notes.RecordId,
                    (x, y) => new
                    {
                        x.Details,
                        x.Tasks,
                        Notes = y.Where(n => !n.Deleted && n.RecordType == Constants.RelatedRecord.Types.Contact)
                    }
                )
                .AsQueryable();

            var count = contacts.Count();

            result.PageCount = count / request.PageInfo.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageInfo.PageNumber;
            result.PageSize = request.PageInfo.PageSize;

            result.Items = contacts.ProjectTo<GetFullContactDto>()
                .GetPagedData(request.PageInfo)
                .ToList();

            return result.AsResponseAsync();
        }
    }

    public class GetFullContactsValidator : AbstractValidator<GetFullContactsRequest>
    {
        public GetFullContactsValidator()
        {
            RuleFor(x => x.PageInfo.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.PageInfo.PageSize)
                .GreaterThan(0);
        }
    }
}
