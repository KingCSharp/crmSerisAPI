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
using crmSeries.Core.Common;

namespace crmSeries.Core.Features.Contacts
{
    [HeavyEquipmentContext]
    public class GetContactsRequest : PagedQueryRequest, IRequest<PagedQueryResult<GetContactDto>>
    {
        /// <summary>
        /// Represents options to return contacts based on their active status.  Leaving
        /// this off the request has will return only active contacts.
        /// </summary>
        public ActiveOptions ActiveOptions { get; set; } = ActiveOptions.ActiveOnly;

        /// <summary>
        /// Any value entered here will return contacts whose
        /// first or last name starts with the value entered in the input box.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// The identifier of the company the contact is associated with.
        /// </summary>
        public int CompanyId { get; set; }
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
                 join company in _context.Set<Company>()
                     on c.CompanyId equals company.CompanyId
                 where !c.Deleted
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
                 .OrderBy(x => x.FirstName)
                 .ThenBy(x => x.LastName)
                 .Distinct()
                 .AsQueryable();

            if (request.ActiveOptions == ActiveOptions.ActiveOnly)
                contacts = contacts.Where(x => x.Active);

            if (request.ActiveOptions == ActiveOptions.InactiveOnly)
                contacts = contacts.Where(x => !x.Active);

            if (!string.IsNullOrEmpty(request.Search))
                contacts = contacts.Where(x => x.FirstName.ToLower().StartsWith(request.Search.ToLower())
                || x.LastName.ToLower().StartsWith(request.Search.ToLower()));

            if (request.CompanyId > 0)
                contacts = contacts.Where(x => x.CompanyId == request.CompanyId);

            var favorites = _context.UserFavoriteRecord
                .Where(x =>
                    x.RecordType == RelatedRecords.Constants.RelatedRecord.Types.Contact &&
                    x.UserId == _identity.RequestingUser.UserId)
                .Select(x => x.RecordId)
                .ToList();

            var count = contacts.Count();

            result.PageCount = (count + request.PageSize - 1) / request.PageSize;
            result.TotalItemCount = count;
            result.PageNumber = request.PageNumber;
            result.PageSize = request.PageSize;

            result.Items = contacts
                .ProjectTo<GetContactDto>()
                .GetPagedData(request)
                .ToList();

            result.Items
                .Where(x => favorites.Contains(x.ContactId))
                .ToList()
                .ForEach(x => { x.Favorite = true; });

            return result.AsResponseAsync();
        }
    }

    public class GetContactsValidator : AbstractValidator<GetContactsRequest>
    {
        public GetContactsValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.CompanyId).GreaterThan(-1);
        }
    }
}
