using System;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Validation;
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
            var exists = _context.Set<Company>().Any(x => x.CompanyId == request.CompanyId);
            if (!exists)
            {
                return Response<AddResponse>
                    .ErrorAsync(CompaniesConstants.ErrorMessages.CompanyIdNotValid);
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
            RuleFor(x => x.CompanyId).GreaterThan(0);

            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.FirstName).MaximumLength(50);

            RuleFor(x => x.MiddleName).MaximumLength(50);

            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(50);

            RuleFor(x => x.NickName).MaximumLength(50);

            RuleFor(x => x.Phone)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.Phone))
                .WithMessage(Constants.ErrorMessages.PhoneInvalid);

            RuleFor(x => x.Cell)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.Cell))
                .WithMessage(Constants.ErrorMessages.PhoneInvalid);

            RuleFor(x => x.Fax)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.Fax))
                .WithMessage(Constants.ErrorMessages.PhoneInvalid);

            RuleFor(x => x.Email)
                .EmailAddress()
                .Unless(x => string.IsNullOrEmpty(x.Email))
                .WithMessage(Constants.ErrorMessages.EmailAddressInvalid);

            RuleFor(x => x.Title).MaximumLength(100);
            RuleFor(x => x.Position).MaximumLength(100);
            RuleFor(x => x.Department).MaximumLength(100);
        }
    }
}