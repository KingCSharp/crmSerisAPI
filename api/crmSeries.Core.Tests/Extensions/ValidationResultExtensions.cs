using System.Linq;
using FluentValidation.Results;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Extensions
{
    public static class ValidationResultExtensions
    {
        public static void AssertIsValid(this ValidationResult result)
        {
            Assert.That(result.Errors.Count, Is.Zero, "Expected there to be no 'Errors'.");
            Assert.That(result.IsValid, Is.True, "Expected 'IsValid' to be true.");
        }

        public static void AssertError(this ValidationResult result, string expectedError)
        {
            Assert.That(result.IsValid, Is.False, "Expected 'IsValid' to be false.");
            Assert.That(result.Errors.Any(x => x.ErrorMessage == expectedError), Is.True,
                $"Expected to find error message '{expectedError}' in list of validation failures.");
        }
    }
}