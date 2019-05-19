using System;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Features.Contacts.Validator;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.Contacts
{
    [HeavyEquipmentContext]
    public class AddContactRequest : AddContactDto, IRequest<AddResponse>
    {
    }

    public class AddContactHandler : IRequestHandler<AddContactRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;

        public AddContactHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<AddResponse>> HandleAsync(AddContactRequest request)
        {
            var exists = _context.Set<Company>()
                .Any(x => x.CompanyId == request.CompanyId);

            if (!exists)
            {
                var errorMessage = CompaniesConstants.ErrorMessages.CompanyIdNotValid;
                return Response<AddResponse>.ErrorAsync(errorMessage);
            }

            var contact = request.MapTo<Contact>();
            contact.Active = true;
            contact.LastModified = DateTime.UtcNow;
            
            _context.Set<Contact>().Add(contact);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = contact.ContactId
            }.AsResponseAsync();
        }
    }

    public class AddContactValidator : AbstractValidator<AddContactRequest>
    {
        public AddContactValidator()
        {
            Include(new BaseContactDtoValidator());
        }
    }
}