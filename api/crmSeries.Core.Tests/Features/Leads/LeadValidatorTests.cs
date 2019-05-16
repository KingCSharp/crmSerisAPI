using crmSeries.Core.Features.Leads;
using crmSeries.Core.Features.Leads.Utility;
using crmSeries.Core.Validation;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace crmSeries.Core.Tests.Features.Leads
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

        [TestCase(null, null, false, LeadsConstants.ErrorMessages.PhoneOrEmailRequired)]
        [TestCase("", "", false, LeadsConstants.ErrorMessages.PhoneOrEmailRequired)]
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

        [TestCase("5555555555", null, null, null, false, LeadsConstants.ErrorMessages.PhoneInvalid)]
        [TestCase("44 1865 722180", null, null, null, false, LeadsConstants.ErrorMessages.PhoneInvalid)]
        [TestCase("2254003348", null, null, null, true)]
        [TestCase("+44 1865 722180", null, null, null, true)]
        [TestCase(null, "5555555555", null, null, false, LeadsConstants.ErrorMessages.CellInvalid)]
        [TestCase(null, "44 1865 722180", null, null, false, LeadsConstants.ErrorMessages.CellInvalid)]
        [TestCase(null, "2254003348", null, null, true)]
        [TestCase(null, "+44 1865 722180", null, null, true)]
        [TestCase(null, null, "5555555555", null, false, LeadsConstants.ErrorMessages.CompanyPhoneInvalid)]
        [TestCase(null, null, "44 1865 722180", null, false, LeadsConstants.ErrorMessages.CompanyPhoneInvalid)]
        [TestCase(null, null, "2254003348", null, true)]
        [TestCase(null, null, "+44 1865 722180", null, true)]
        [TestCase(null, null, null, "5555555555", false, LeadsConstants.ErrorMessages.FaxInvalid)]
        [TestCase(null, null, null, "44 1865 722180", false, LeadsConstants.ErrorMessages.FaxInvalid)]
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

        [TestCase("The Company Name For This Test Is Over One Hundred Characters Long And Should Throw An Error Message Stating That", false)]
        [TestCase("Normal Company Name", true)]
        public void Validate_MaximumLengthCompanyName_ReturnsAppropriateErrorMessage
            (
            string companyName,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.CompanyName = companyName;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Company Name"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("The Contact's Name For This Test Is Over One Hundred Characters Long And Should Throw An Error Message Stating That", false)]
        [TestCase("John Smith", true)]
        public void Validate_MaximumLengthName_ReturnsAppropriateErrorMessage
            (
            string name,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Name = name;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Name"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("+1 555-555-555-555-5555", false)]
        [TestCase("2254003348", true)]
        public void Validate_MaximumLengthPhone_ReturnsAppropriateErrorMessage
            (
            string phone,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Phone = phone;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Phone"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[1].ErrorMessage));
            }
        }

        [TestCase("john_this_is_an_email_that_is_over_one_hundred_characters_in_length_and_is_too_long_for_us_to_deem_valid@smith.com", false)]
        [TestCase("john@smith.com", true)]
        public void Validate_MaximumLengthEmail_ReturnsAppropriateErrorMessage
            (
            string email,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Email = email;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Email"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean rhoncus sed eros non tristique. Duis tempus, nunc ut laoreet sagittis, nunc tortor tincidunt lorem, vel accumsan mauris metus vel sapien. Praesent pretium volutpat est, maximus. Nunc sit amet nisi volutpat.", false)]
        [TestCase("This is a description of a lead.", true)]
        public void Validate_MaximumLengthDescription_ReturnsAppropriateErrorMessage
            (
            string description,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Description = description;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Description"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("123 Street vel leo sit amet turpis mollis semper eget et sapien scelerisque mattis urna eu vulputate Drive", false)]
        [TestCase("123 Street Drive", true)]
        public void Validate_MaximumLengthAddress1_ReturnsAppropriateErrorMessage
            (
            string address1,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Address1 = address1;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z0-9\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Address1"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("Lorem Ipsum Dolor Sit Amet Consectetur Adipiscing Elit", false)]
        [TestCase("Baton Rouge", true)]
        public void Validate_MaximumLengthCity_ReturnsAppropriateErrorMessage
            (
            string city,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.City = city;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("City"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("Lorem Ipsum Dolor Sit Amet Consectetur Adipiscing Elit", false)]
        [TestCase("Louisiana", true)]
        public void Validate_MaximumLengthState_ReturnsAppropriateErrorMessage
            (
            string state,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.State = state;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("State"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("12345678901", false)]
        [TestCase("70808", true)]
        public void Validate_MaximumLengthZip_ReturnsAppropriateErrorMessage
            (
            string zip,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Zip = zip;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Zip"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("Lorem ipsum dolor sit amet consectetur adipiscing elit", false)]
        [TestCase("East Baton Rouge", true)]
        public void Validate_MaximumLengthCounty_ReturnsAppropriateErrorMessage
            (
            string county,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.County = county;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("County"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("I pledge allegiance to the flag of the United States of America", false)]
        [TestCase("United States", true)]
        public void Validate_MaximumLengthCountry_ReturnsAppropriateErrorMessage
            (
            string country,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Country = country;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Country"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("+1 555-555-555-555-5555", false)]
        [TestCase("2254003348", true)]
        public void Validate_MaximumLengthCell_ReturnsAppropriateErrorMessage
            (
            string cell,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Cell = cell;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Cell"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[1].ErrorMessage));
            }
        }

        [TestCase("123 Street vel leo sit amet turpis mollis semper eget et sapien scelerisque mattis urna eu vulputate Drive", false)]
        [TestCase("123 Street Drive", true)]
        public void Validate_MaximumLengthAddress2_ReturnsAppropriateErrorMessage
            (
            string address2,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Address2 = address2;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z0-9\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Address2"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("+1 555-555-555-555-555-555-555-555-555-555-555-555-555-555-555-555-555-555-555-555-555-555-555-555-5555", false)]
        [TestCase("2254003348", true)]
        public void Validate_MaximumLengthCompanyPhone_ReturnsAppropriateErrorMessage
            (
            string companyPhone,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.CompanyPhone = companyPhone;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Company Phone"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[1].ErrorMessage));
            }
        }

        [TestCase("http://www.goooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooogle.com"
            , false)]
        [TestCase("http://www.google.com", true)]
        public void Validate_MaximumLengthWeb_ReturnsAppropriateErrorMessage
            (
            string web,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Web = web;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Web"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("+1 555-555-555-555-555-555-555-555-555-555-555-5555", false)]
        [TestCase("2254003348", true)]
        public void Validate_MaximumLengthFax_ReturnsAppropriateErrorMessage
            (
            string fax,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Fax = fax;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Fax"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[1].ErrorMessage));
            }
        }

        [TestCase("Mister Doctor", false)]
        [TestCase("Mr.", true)]
        public void Validate_MaximumLengthTitle_ReturnsAppropriateErrorMessage
            (
            string title,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Title = title;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Title"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("Head of the Board of Directors, Chief Financial Officer, Chief Operational Officer, Head of Quality Assurance", false)]
        [TestCase("CEO", true)]
        public void Validate_MaximumLengthPosition_ReturnsAppropriateErrorMessage
            (
            string position,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Position = position;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Position"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }

        [TestCase("Department of Incredibly Long Department Name Generation Including But Not Limited to Departments Near You", false)]
        [TestCase("Sales", true)]
        public void Validate_MaximumLengthDepartment_ReturnsAppropriateErrorMessage
            (
            string department,
            bool isValid)
        {
            // Arrange
            _addLeadRequest.Department = department;
            var lengthErrorRegex = new Regex(@"(The\slength\sof\s\'[A-z\s]+\'\smust\sbe\s[0-9]+\scharacters\sor\sfewer.\sYou\sentered\s[0-9]+\scharacters.)");

            // Act
            var result = _addLeadValidator.Validate(_addLeadRequest);

            // Assert
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains("Department"));
                Assert.IsTrue(lengthErrorRegex.IsMatch(result.Errors[0].ErrorMessage));
            }
        }
    }
}