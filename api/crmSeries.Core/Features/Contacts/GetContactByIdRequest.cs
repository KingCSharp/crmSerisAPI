using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using FluentValidation;
using static crmSeries.Core.Features.RelatedRecords.Constants;

namespace crmSeries.Core.Features.Contacts
{
    [HeavyEquipmentContext]
    public class GetContactByIdRequest : IRequest<GetContactDto>
    {
        public GetContactByIdRequest(int contactId)
        {
            ContactId = contactId;
        }
        public int ContactId { get; private set; }
    }

    public class GetContactByIdHandler : IRequestHandler<GetContactByIdRequest, GetContactDto>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identityUserContext;

        public GetContactByIdHandler(HeavyEquipmentContext context,
            IIdentityUserContext identityUserContext)
        {
            _context = context;
            _identityUserContext = identityUserContext;
        }

        public Task<Response<GetContactDto>> HandleAsync(GetContactByIdRequest request)
        {
            var contact = (from c in _context.Set<Contact>()
                           join company in _context.Set<Company>()
                               on c.CompanyId equals company.CompanyId
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
                .ProjectTo<GetContactDto>()
                .SingleOrDefault(x => x.ContactId == request.ContactId);

            if (contact != null)
            {
                var favorites = _context.UserFavoriteRecord
                .Where(x =>
                    x.RecordType == RelatedRecord.Types.Contact &&
                    x.UserId == _identityUserContext.RequestingUser.UserId)
                .Select(x => x.RecordId)
                .ToList();

                var isRecordFavorited = favorites.Contains(request.ContactId);

                contact.Favorite = isRecordFavorited;
            }
            return contact.AsResponseAsync();
        }
    }

    public class GetContactByIdValidator : AbstractValidator<GetContactByIdRequest>
    {
        public GetContactByIdValidator()
        {
            RuleFor(x => x.ContactId).GreaterThan(0);
        }
    }
}