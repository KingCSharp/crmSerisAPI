using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Validation
{
    public class UrlValidator : PropertyValidator
    {
        public UrlValidator()
            : base("{PropertyName} must be a valid phone number")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var urlInput = context.PropertyValue as string;
            return Uri.IsWellFormedUriString(urlInput, UriKind.RelativeOrAbsolute);
        }
    }
}
