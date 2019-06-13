using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Contacts
{
    [TestFixture]
    public class GetContactByIdHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssues_ReturnsContact()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var company = new Company
                {
                    CompanyId = 1,
                    CompanyName = "Foo Company"
                };

                context.Company.Add(company);

                var contact = new Contact
                {
                    ContactId = 1,
                    CompanyId = 1
                };

                context.Contact.Add(contact);

                context.SaveChanges();
                var handler = new GetContactByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetContactByIdRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.ContactId, contact.ContactId);
                Assert.AreEqual(response.Result.Data.CompanyName, company.CompanyName);
            }
        }

        [Test]
        public void HandleAsync_NoContactFound_ReturnsEmptyData()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new GetContactByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetContactByIdRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNull(response.Result.Data);
            }
        }
    }
}