using crmSeries.Core.Common;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Validation;
using FluentValidation;

namespace crmSeries.Core.Features.Contacts.Validator
{
    public class BaseCompanyDtoValidator : AbstractValidator<BaseCompanyDto>
    {
        public BaseCompanyDtoValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.CompanyName).MaximumLength(100);

            RuleFor(x => x.AccountNo).NotEmpty();
            RuleFor(x => x.AccountNo).MaximumLength(50);

            RuleFor(x => x.LegalName).MaximumLength(2000);

            RuleFor(x => x.Address1).MaximumLength(100);
            RuleFor(x => x.Address2).MaximumLength(100);
            RuleFor(x => x.Address3).MaximumLength(100);

            RuleFor(x => x.City).MaximumLength(50);
            RuleFor(x => x.State).MaximumLength(50);
            RuleFor(x => x.Zip).MaximumLength(10);

            RuleFor(x => x.County).MaximumLength(50);
            RuleFor(x => x.Country).MaximumLength(25);

            RuleFor(x => x.Status).MaximumLength(50);

            RuleFor(x => x.Web).MaximumLength(100);

            RuleFor(x => x.Phone)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.Phone))
                .WithMessage(Constants.ErrorMessages.PhoneInvalid);

            RuleFor(x => x.Fax)
                .SetValidator(new PhoneNumberValidator())
                .Unless(x => string.IsNullOrEmpty(x.Fax))
                .WithMessage(Constants.ErrorMessages.PhoneInvalid);
        }
    }
}
