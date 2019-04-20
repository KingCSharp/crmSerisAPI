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

            RuleFor(x => x.CompanyName)
                .MaximumLength(100);

            RuleFor(x => x.FirstName)
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .MaximumLength(50);

            RuleFor(x => x.Phone)
                .MaximumLength(20);

            RuleFor(x => x.Email)
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(250);

            RuleFor(x => x.Address1)
                .MaximumLength(100);

            RuleFor(x => x.City)
                .MaximumLength(50);

            RuleFor(x => x.State)
                .MaximumLength(50);

            RuleFor(x => x.Zip)
                .MaximumLength(10);

            RuleFor(x => x.County)
                .MaximumLength(50);

            RuleFor(x => x.Country)
                .MaximumLength(25);

            RuleFor(x => x.Cell)
                .MaximumLength(20);

            RuleFor(x => x.Address2)
                .MaximumLength(100);

            RuleFor(x => x.CompanyPhone)
                .MaximumLength(100);

            RuleFor(x => x.Web)
                .MaximumLength(200);

            RuleFor(x => x.Fax)
                .MaximumLength(50);

            RuleFor(x => x.Title)
                .MaximumLength(10);

            RuleFor(x => x.Position)
                .MaximumLength(100);

            RuleFor(x => x.Department)
                .MaximumLength(100);
        }
    }
}
