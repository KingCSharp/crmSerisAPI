using System.Linq;
using crmSeries.Core.Mediator;
using Newtonsoft.Json;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Extensions
{
    public static class ResponseExtensions
    {
        public static void AssertIsValid(this Response response)
        {
            var messages = response.Errors
                .Select(x =>
                    $"{(string.IsNullOrEmpty(x.PropertyName) ? string.Empty : string.Concat(x.PropertyName, ": "))}{x.ErrorMessage}");

            Assert.That(response.HasErrors, Is.False,
                string.Concat("Response has one or more errors.\r\n- ", string.Join("\r\n- ", messages)));
        }

        public static void AssertError(this Response response, string expectedMessage, string forProperty = "")
        {
            var propertyErrors = response.Errors.Where(x => x.PropertyName == forProperty).ToArray();

            Assert.That(propertyErrors.Length > 1, Is.False,
                $"Expected 1 error for {forProperty} but found {propertyErrors.Length}." +
                JsonConvert.SerializeObject(propertyErrors));

            var error = propertyErrors.FirstOrDefault(x => x.ErrorMessage == expectedMessage);

            Assert.That(error, Is.Not.Null,
                string.IsNullOrEmpty(forProperty)
                    ? $"Expected error was not found: {expectedMessage}"
                    : $"Expected error was not found for {forProperty}: {expectedMessage}");
        }
    }
}