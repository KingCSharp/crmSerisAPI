using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using crmSeries.Core.Features.Contacts;
using NUnit.Framework;


namespace crmSeries.Unit.Tests.Companies
{
    [TestFixture]
    public class GetContactByIdHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_ReturnsContact()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions("NormalRequest_NoIssues_ReturnsContact");

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Contact.Add(new Contact { ContactId = 1 });
                context.SaveChanges();
                var handler = new GetContactByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetContactByIdRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.ContactId, 1);
            }
        }

        [Test]
        public void NormalRequest_NoContactFound_ReturnsEmptyData()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions("NormalRequest_NoContactFound_ReturnsEmptyData");

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