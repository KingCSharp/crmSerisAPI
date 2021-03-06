using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Features.Contacts.Utility;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Contacts
{
    [TestFixture]
    public class DeleteContactHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssues_ContactInactiveAndDeleted()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Contact.Add(new Contact
                {
                    ContactId = 1,
                    Active = true,
                    Deleted = false
                });
                context.SaveChanges();

                var handler = new DeleteContactHandler(context);

                // Act
                var response = handler.HandleAsync(new DeleteContactRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
            }
            
            using (var context = new HeavyEquipmentContext(options))
            {
                var contact = context.Contact.SingleOrDefault(x => x.ContactId == 1);
                Assert.IsNotNull(contact);
                Assert.AreEqual(contact.Active, false);
                Assert.AreEqual(contact.Deleted, true);
            }
        }

        [Test]
        public void HandleAsync_NoContactMatchingId_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new DeleteContactHandler(context);

                // Act
                var response = handler.HandleAsync(new DeleteContactRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    ContactsConstants.ErrorMessages.ContactNotFound);
            }
        }
    }
}