using crmSeries.Core.Features.Leads;
using crmSeries.Core.Validation;
using NUnit.Framework;

namespace crmSeries.Tests.Core.Features.Leads
{
    [TestFixture]
    public class LeadValidatorTests
    {
        private AddLeadValidator _addLeadValidator;
        private AddLeadRequest _addLeadRequest;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _addLeadValidator = new AddLeadValidator();
        }

        [SetUp]
        public void Setup()
        {
            _addLeadRequest = new AddLeadRequest
            {
                Name = "John Doe"
            };
        }

        [TestCase(null, null, false, ErrorMessages.Leads.PhoneOrEmailRequired)]
        [TestCase("", "", false, ErrorMessages.Leads.PhoneOrEmailRequired)]
        [TestCase("5042599759", "", true)]
        [TestCase("5042599759", null, true)]
        [TestCase("", "foo@foobar.com", true)]
        [TestCase(null, "foo@foobar.com", true)]
        public void Validate_EmailOrPhoneNumberMissing_ReturnsAppropriateErrorMessage(
            string phone,
            string email,
            bool isValid,
            string errorMessage = null)
        {
            // Arrange 
            _addLeadRequest.Email = email;
            _addLeadRequest.Phone = phone;

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
                Assert.AreEqual( errorMessage, result.Errors[0].ErrorMessage);
        }
    }
}