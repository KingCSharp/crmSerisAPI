using crmSeries.Core.Features.Contacts;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Companies
{
    [TestFixture]
    public class DeleteContactValidatorTests
    {
        private DeleteContactRequest _deleteContactRequest;
        private DeleteContactValidator _deleteContactValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _deleteContactValidator = new DeleteContactValidator();
        }

        [SetUp]
        public void Setup()
        {
            _deleteContactRequest = new DeleteContactRequest(1);
        }

        [TestCase(1)]
        [TestCase(35)]
        [TestCase(5783)]
        public void Validate_ValidCompanyId_IsValid(int contactId)
        {
            // Arrange 
            _deleteContactRequest = new DeleteContactRequest(contactId);

            // Act
            var result = _deleteContactValidator.Validate(_deleteContactRequest);

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
            _deleteContactRequest = new DeleteContactRequest(contactId);

            // Act
            var result = _deleteContactValidator.Validate(_deleteContactRequest);

            //Assert 
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(result.Errors.Count, 0);
        }
    }
}
