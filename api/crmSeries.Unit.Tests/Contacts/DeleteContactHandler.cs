using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Features.Leads.Utility;
using NUnit.Framework;
using System.Linq;

namespace crmSeries.Unit.Tests.Companies
{
    [TestFixture]
    public class DeleteContactHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_ContactInactiveAndDeleted()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions(
                "NormalRequest_NoIssues_ContactInactiveAndDeleted");

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
        public void NormalRequest_NoContactMatchingId_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions(
                "NormalRequest_NoContactMatchingId_ReturnsAppropriateError");

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