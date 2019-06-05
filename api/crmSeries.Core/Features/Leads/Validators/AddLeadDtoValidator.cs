using crmSeries.Core.Common;
using crmSeries.Core.Features.Leads.Dtos;
using crmSeries.Core.Validation;
using FluentValidation;

namespace crmSeries.Core.Features.Leads.Validators
{
    public class AddLeadDtoValidator : AbstractValidator<AddLeadDto>
    {
        public AddLeadDtoValidator()
        {
            RuleFor(x => x)
                .Must(IncludePhoneNumberOrEmail)
                .WithMessage(Constants.ErrorMessages.PhoneOrEmailRequired);

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress()
                .Unless(x => string.IsNullOrEmpty(x.Email))
                .WithMessage(Constants.ErrorMessages.EmailAddressInvalid);

            RuleFor(x => x.OwnerEmail)
                .EmailAddress()
                .Unless(x => string.IsNullOrEmpty(x.Email))
                .WithMessage(Constants.ErrorMessages.EmailAddressInvalid);

            RuleFor(x => x.Phone)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.Phone))
                .WithMessage(Constants.ErrorMessages.PhoneInvalid);

            RuleFor(x => x.Cell)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.Cell))
                .WithMessage(Constants.ErrorMessages.CellInvalid);

            RuleFor(x => x.Fax)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.Fax))
                .WithMessage(Constants.ErrorMessages.FaxInvalid);

            RuleFor(x => x.CompanyPhone)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.CompanyPhone))
                .WithMessage(Constants.ErrorMessages.CompanyPhoneInvalid);

            RuleFor(x => x.CompanyName)
                .MaximumLength(100);

            RuleFor(x => x.Name)
                .MaximumLength(100);

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

        private bool IncludePhoneNumberOrEmail(AddLeadDto dto)
        {
            var result = !string.IsNullOrEmpty(dto.Phone) ||
                   !string.IsNullOrEmpty(dto.Email);

            return result;
        }
    }
}
