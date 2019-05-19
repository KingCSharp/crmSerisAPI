using System;
using System.Collections.Generic;
using System.Text;
using crmSeries.Core.Common;
using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Validation;
using FluentValidation;

namespace crmSeries.Core.Features.Contacts.Validator
{
    public class BaseContactDtoValidator : AbstractValidator<BaseContactDto>
    {
        public BaseContactDtoValidator()
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
