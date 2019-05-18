using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using NUnit.Framework;


namespace crmSeries.Unit.Tests.Companies
{
    [TestFixture]
    public class GetCompanyRequestHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_ReturnsCompany()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions("NormalRequest_NoIssues_ReturnsCompany");

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Company.Add(new Company { CompanyId = 1 });
                context.SaveChanges();
                var handler = new GetCompanyRequestHandler(context);

                // Act
                var response = handler.HandleAsync(new GetCompanyRequest { CompanyId = 1 });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.CompanyId, 1);
            }
        }

        [Test]
        public void NormalRequest_NoCompanyFound_ReturnsEmptyData()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions("NormalRequest_NoCompanyFound_ReturnsEmptyData");

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new GetCompanyRequestHandler(context);

                // Act
                var response = handler.HandleAsync(new GetCompanyRequest { CompanyId = 1 });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNull(response.Result.Data);
            }
        }
    }
}