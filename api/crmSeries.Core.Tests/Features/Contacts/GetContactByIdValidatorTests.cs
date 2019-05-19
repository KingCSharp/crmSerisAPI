using crmSeries.Core.Features.Contacts;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Contacts
{
    [TestFixture]
    public class GetContactByIdValidatorTests
    {
        private GetContactByIdRequest _getContactByIdRequest;
        private GetContactByIdValidator _getContactByIdValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _getContactByIdValidator = new GetContactByIdValidator();
        }

        [SetUp]
        public void Setup()
        {
            _getContactByIdRequest = new GetContactByIdRequest(1);
        }

        [TestCase(1)]
        [TestCase(35)]
        [TestCase(5783)]
        public void Validate_ValidCompanyId_IsValid(int contactId)
        {
            // Arrange 
            _getContactByIdRequest = new GetContactByIdRequest(contactId);

            // Act
            var result = _getContactByIdValidator.Validate(_getContactByIdRequest);

            //Assert 
            Assert.AreEqual(true, result.IsValid);
            Assert.AreEqual(result.Errors.Count, 0);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(null)]
        public void Validate_InvalidCompanyId_ReturnsAppropriateMessage(int contactId)
        {
            // Arrange 
            _getContactByIdRequest = new GetContactByIdRequest(contactId);

            // Act
            var result = _getContactByIdValidator.Validate(_getContactByIdRequest);

            //Assert 
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(result.Errors.Count, 0);
        }
    }
}
