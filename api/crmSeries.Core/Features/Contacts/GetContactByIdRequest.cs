using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

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

        public GetContactByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetContactDto>> HandleAsync(GetContactByIdRequest request)
        {
            return (from c in _context.Set<Contact>()
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
                        c.LastModified,
                        company.CompanyName
                    })
                .ProjectTo<GetContactDto>()
                .SingleOrDefault(x => x.ContactId == request.ContactId)
                .AsResponseAsync();
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