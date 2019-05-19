using crmSeries.Core.Features.Contacts;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Contacts
{
    [TestFixture]
    public class EditContactValidatorTests
    {
        [TestCase(1, 0, true)]
        [TestCase(-1, 1, false)]
        [TestCase(0, 1, false)]
        public void Validate_CompanyId_ReturnsAppropriate(int contactId,
            int numberOfErros,
            bool isValid)
        {
            //Arrange
            var editContactRequest = new EditContactRequest
            {
                CompanyId = 1,
                FirstName = "John",
                LastName = "Doe",
                ContactId = contactId
            };

            // Act
            var result = new EditContactValidator().Validate(editContactRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErros, result.Errors.Count);

            if (!result.IsValid)
            {
                Assert.AreEqual("'Contact Id' must be greater than '0'.", result.Errors[0].ErrorMessage);
            }
        }
    }
}