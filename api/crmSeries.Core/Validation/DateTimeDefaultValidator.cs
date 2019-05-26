using FluentValidation.Validators;
using System;

namespace crmSeries.Core.Validation
{
    public class DateTimeDefaultValidator : PropertyValidator
    {
        public DateTimeDefaultValidator()
            :base("{PropertyName} must be a date time value.")

        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            try
            {
                var dateInput = (DateTime) context.PropertyValue;
                return !dateInput.Equals(default);
            }
            catch
            {
                return false;
            }
        }
    }
}