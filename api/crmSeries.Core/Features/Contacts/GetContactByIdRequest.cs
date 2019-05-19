using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
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
            return _context.Contact
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