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
                Name = "John Doe",
                Email = "john@doe.com",
                Phone = "2254003348"
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

        [TestCase("5555555555", null, null, null, false, ErrorMessages.Leads.PhoneInvalid)]
        [TestCase("44 1865 722180", null, null, null, false, ErrorMessages.Leads.PhoneInvalid)]
        [TestCase("2254003348", null, null, null, true)]
        [TestCase("+44 1865 722180", null, null, null, true)]
        [TestCase(null, "5555555555", null, null, false, ErrorMessages.Leads.CellInvalid)]
        [TestCase(null, "44 1865 722180", null, null, false, ErrorMessages.Leads.CellInvalid)]
        [TestCase(null, "2254003348", null, null, true)]
        [TestCase(null, "+44 1865 722180", null, null, true)]
        [TestCase(null, null, "5555555555", null, false, ErrorMessages.Leads.CompanyPhoneInvalid)]
        [TestCase(null, null, "44 1865 722180", null, false, ErrorMessages.Leads.CompanyPhoneInvalid)]
        [TestCase(null, null, "2254003348", null, true)]
        [TestCase(null, null, "+44 1865 722180", null, true)]
        [TestCase(null, null, null, "5555555555", false, ErrorMessages.Leads.FaxInvalid)]
        [TestCase(null, null, null, "44 1865 722180", false, ErrorMessages.Leads.FaxInvalid)]
        [TestCase(null, null, null, "2254003348", true)]
        [TestCase(null, null, null, "+44 1865 722180", true)]
        public void Validate_PhoneNumbersInvalid_ReturnsAppropriateErrorMessage(
            string phone,
            string cell,
            string companyPhone,
            string fax,
            bool isValid,
            string errorMessage = null)
        {
            // Arrange
            _addLeadRequest.Phone = phone;
            _addLeadRequest.Cell = cell;
            _addLeadRequest.CompanyPhone = companyPhone;
            _addLeadRequest.Fax = fax;

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
                Assert.AreEqual(errorMessage, result.Errors[0].ErrorMessage);
        }

        [TestCase(null, false, "'Name' must not be empty.")]
        [TestCase("", false, "'Name' must not be empty.")]
        [TestCase(" ", false, "'Name' must not be empty.")]
        [TestCase("         ", false, "'Name' must not be empty.")]
        [TestCase(" ", false, "'Name' must not be empty.")]
        [TestCase("     ", false, "'Name' must not be empty.")]
        [TestCase("John Doe", true)]
        [TestCase("John", true)]
        public void Validate_NameEmpty_ReturnsAppropriateErrorMessage(
            string name,
            bool isValid,
            string errorMessage = null)
        {
            // Arrange
            _addLeadRequest.Name = name;

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
                Assert.AreEqual(errorMessage, result.Errors[0].ErrorMessage);
        }

        
    }
}