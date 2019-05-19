using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Features.Contacts.Validator;
using crmSeries.Core.Features.Leads.Utility;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Contacts
{
    [TestFixture]
    public class BaseContactDtoValidatorTests
    {
        private BaseContactDto _baseContactDto;
        private BaseContactDtoValidator _baseContactDtoValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _baseContactDtoValidator = new BaseContactDtoValidator();
        }

        [SetUp]
        public void Setup()
        {
            _baseContactDto = new BaseContactDto()
            {
                CompanyId = 1,
                FirstName = "John",
                LastName = "Doe",
            };
        }

        [Test]
        public void Validate_MiniumRequiredFieldsPresent_IsValid()
        {
            // Act
            var result = _baseContactDtoValidator.Validate(_baseContactDto);

            //Assert 
            Assert.AreEqual(true, result.IsValid);
            Assert.AreEqual(result.Errors.Count, 0);
        }

        [TestCase(1, 0, true)]
        [TestCase(-1, 1, false)]
        [TestCase(0, 1, false)]
        public void Validate_CompanyId_ReturnsAppropriate(int companyId,
            int numberOfErros,
            bool isValid)
        {
            //Arrange
            _baseContactDto.CompanyId = companyId;

            // Act
            var result = _baseContactDtoValidator.Validate(_baseContactDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErros, result.Errors.Count);

            if (!result.IsValid)
            {
                Assert.AreEqual("'Company Id' must be greater than '0'.", result.Errors[0].ErrorMessage);
            }
        }


        [TestCase(ContactsConstants.MaxFirstNameLength - 1, 0, true)]
        [TestCase(ContactsConstants.MaxFirstNameLength, 0, true)]
        [TestCase(ContactsConstants.MaxFirstNameLength + 1, 1, false)]
        public void Validate_FirstNameLength_ReturnsAppropriate(int stringLength,
            int numberOfErros,
            bool isValid)
        {
            //Arrange
            _baseContactDto.FirstName = new string('A', stringLength);

            // Act
            var result = _baseContactDtoValidator.Validate(_baseContactDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErros, result.Errors.Count);

            if (!result.IsValid)
            {
                var errorMessage = $"The length of 'First Name' must be {ContactsConstants.MaxFirstNameLength} characters or fewer.";
                Assert.IsTrue(result.Errors[0].ErrorMessage.StartsWith(errorMessage));
            }
        }

        [TestCase(ContactsConstants.MaxMiddleNameLength - 1, 0, true)]
        [TestCase(ContactsConstants.MaxMiddleNameLength, 0, true)]
        [TestCase(ContactsConstants.MaxMiddleNameLength + 1, 1, false)]
        public void Validate_MiddleNameExceedMaxLength_IsInvalid(int stringLength,
            int numberOfErros,
            bool isValid)
        {
            //Arrange
            _baseContactDto.MiddleName = new string('A', stringLength);

            // Act
            var result = _baseContactDtoValidator.Validate(_baseContactDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErros, result.Errors.Count);

            if (!result.IsValid)
            {
                var errorMessage = $"The length of 'Middle Name' must be {ContactsConstants.MaxMiddleNameLength} characters or fewer.";
                Assert.IsTrue(result.Errors[0].ErrorMessage.StartsWith(errorMessage));
            }
        }

        [TestCase(ContactsConstants.MaxLastNameLength - 1, 0, true)]
        [TestCase(ContactsConstants.MaxLastNameLength, 0, true)]
        [TestCase(ContactsConstants.MaxLastNameLength + 1, 1, false)]
        public void Validate_LastNameLength_ReturnsAppropriate(int stringLength,
            int numberOfErros,
            bool isValid)
        {
            //Arrange
            _baseContactDto.LastName = new string('A', stringLength);

            // Act
            var result = _baseContactDtoValidator.Validate(_baseContactDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErros, result.Errors.Count);

            if (!result.IsValid)
            {
                var errorMessage = $"The length of 'Last Name' must be {ContactsConstants.MaxLastNameLength} characters or fewer.";
                Assert.IsTrue(result.Errors[0].ErrorMessage.StartsWith(errorMessage));
            }
        }

        [TestCase(ContactsConstants.MaxNickNameLength - 1, 0, true)]
        [TestCase(ContactsConstants.MaxNickNameLength, 0, true)]
        [TestCase(ContactsConstants.MaxNickNameLength + 1, 1, false)]
        public void Validate_NickNameLength_ReturnsAppropriate(int stringLength,
            int numberOfErros,
            bool isValid)
        {
            //Arrange
            _baseContactDto.NickName = new string('A', stringLength);

            // Act
            var result = _baseContactDtoValidator.Validate(_baseContactDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErros, result.Errors.Count);

            if (!result.IsValid)
            {
                var errorMessage = $"The length of 'Nick Name' must be {ContactsConstants.MaxNickNameLength} characters or fewer.";
                Assert.IsTrue(result.Errors[0].ErrorMessage.StartsWith(errorMessage));
            }
        }

        [TestCase(ContactsConstants.MaxTitleLength - 1, 0, true)]
        [TestCase(ContactsConstants.MaxTitleLength, 0, true)]
        [TestCase(ContactsConstants.MaxTitleLength + 1, 1, false)]
        public void Validate_TitleLength_ReturnsAppropriate(int stringLength,
            int numberOfErros,
            bool isValid)
        {
            //Arrange
            _baseContactDto.Title = new string('A', stringLength);

            // Act
            var result = _baseContactDtoValidator.Validate(_baseContactDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErros, result.Errors.Count);

            if (!result.IsValid)
            {
                var errorMessage = $"The length of 'Title' must be {ContactsConstants.MaxTitleLength} characters or fewer.";
                Assert.IsTrue(result.Errors[0].ErrorMessage.StartsWith(errorMessage));
            }
        }

        [TestCase(ContactsConstants.MaxPositionLength - 1, 0, true)]
        [TestCase(ContactsConstants.MaxPositionLength, 0, true)]
        [TestCase(ContactsConstants.MaxPositionLength + 1, 1, false)]
        public void Validate_PositionLength_ReturnsAppropriate(int stringLength,
            int numberOfErros,
            bool isValid)
        {
            //Arrange
            _baseContactDto.Position = new string('A', stringLength);

            // Act
            var result = _baseContactDtoValidator.Validate(_baseContactDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErros, result.Errors.Count);

            if (!result.IsValid)
            {
                var errorMessage = $"The length of 'Position' must be {ContactsConstants.MaxPositionLength} characters or fewer.";
                Assert.IsTrue(result.Errors[0].ErrorMessage.StartsWith(errorMessage));
            }
        }

        [TestCase(ContactsConstants.MaxDepartmentLength - 1, 0, true)]
        [TestCase(ContactsConstants.MaxDepartmentLength, 0, true)]
        [TestCase(ContactsConstants.MaxDepartmentLength + 1, 1, false)]
        public void Validate_DepartmentLength_ReturnsAppropriate(int stringLength,
            int numberOfErros,
            bool isValid)
        {
            //Arrange
            _baseContactDto.Department = new string('A', stringLength);

            // Act
            var result = _baseContactDtoValidator.Validate(_baseContactDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErros, result.Errors.Count);

            if (!result.IsValid)
            {
                var errorMessage = $"The length of 'Department' must be {ContactsConstants.MaxDepartmentLength} characters or fewer.";
                Assert.IsTrue(result.Errors[0].ErrorMessage.StartsWith(errorMessage));
            }
        }
    }
}