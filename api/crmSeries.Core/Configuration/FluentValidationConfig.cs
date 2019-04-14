using FluentValidation;

namespace crmSeries.Core.Configuration
{
    public static class FluentValidationConfig
    {
        public static void Configure()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
