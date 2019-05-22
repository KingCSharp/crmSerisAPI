using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using crmSeries.Core.Features.Companies.Utility;
using NUnit.Framework;


namespace crmSeries.Core.Tests.Features.Companies
{
    [TestFixture]
    public class GetCompanyFullRequestHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_ReturnsFullCompany()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Company.Add(new Company { CompanyId = 1 });
                context.CompanyAssignedAddress.Add(new CompanyAssignedAddress { AddressId = 1, CompanyId = 1 });
                context.Contact.Add(new Contact { ContactId = 1, CompanyId = 1, Active = true, Deleted = false });
                context.SaveChanges();

                var handler = new GetCompanyFullRequestHandler(context);

                // Act
                var response = handler.HandleAsync(new GetCompanyFullRequest { CompanyId = 1 });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.Details.CompanyId, 1);
                Assert.AreEqual(response.Result.Data.Addresses.Count, 1);
                Assert.AreEqual(response.Result.Data.Contacts.Count, 1);
            }
        }

        [Test]
        public void NormalRequest_CompanyIsNull_ReturnsAppropriateErrorMessage()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new GetCompanyFullRequestHandler(context);

                // Act
                var response = handler.HandleAsync(new GetCompanyFullRequest { CompanyId = 1 });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.IsNull(response.Result.Data);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage, 
                    CompaniesConstants.ErrorMessages.CompanyNotFound);
            }
        }
    }
}