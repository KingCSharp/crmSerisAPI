using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Validation
{
    public class PhoneNumberValidator : PropertyValidator
    {
        public PhoneNumberValidator()
            :base("{PropertyName} must be a valid phone number")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            try
            {
                var phoneInput = context.PropertyValue as string;
                var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
                var phoneNumberParsed = phoneNumberUtil.Parse(phoneInput, phoneInput.Contains("+") ? null : "US");
                return phoneNumberUtil.IsValidNumber(phoneNumberParsed);
            }
            catch
            {
                return false;
            }
        }
    }
}
