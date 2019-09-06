using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Security;
using NSubstitute;
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

                var identityContext = Substitute.For<IIdentityUserContext>();
                identityContext.RequestingUser.Returns(x => new IdentityUser
                {
                    UserId = 1
                });

                var handler = new GetContactByIdHandler(context, identityContext);

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

            var identityContext = Substitute.For<IIdentityUserContext>();
            identityContext.RequestingUser.Returns(x => new IdentityUser
            {
                UserId = 1
            });

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Contact.Add(new Contact
                {
                    ContactId = 2
                });

                var handler = new GetContactByIdHandler(context, identityContext);

                // Act
                var response = handler.HandleAsync(new GetContactByIdRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNull(response.Result.Data);
            }
        }
    }
}