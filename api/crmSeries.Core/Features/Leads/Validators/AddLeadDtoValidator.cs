using crmSeries.Core.Dtos;
using crmSeries.Core.Validation;
using FluentValidation;

namespace crmSeries.Core.Features.Leads.Validators
{
    public class AddLeadDtoValidator : AbstractValidator<AddLeadDto>
    {
        public AddLeadDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.CompanyName)
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.Phone)
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(ErrorMessages.Leads.EmailAddressInvalid);

            RuleFor(x => x.Phone)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.Phone))
                .WithMessage(ErrorMessages.Leads.PhoneInvalid);

            RuleFor(x => x.Cell)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.Cell))
                .WithMessage(ErrorMessages.Leads.CellInvalid);

            RuleFor(x => x.Fax)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.Fax))
                .WithMessage(ErrorMessages.Leads.FaxInvalid);

            RuleFor(x => x.CompanyPhone)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.CompanyPhone))
                .WithMessage(ErrorMessages.Leads.CompanyPhoneInvalid);
        }
    }
}
