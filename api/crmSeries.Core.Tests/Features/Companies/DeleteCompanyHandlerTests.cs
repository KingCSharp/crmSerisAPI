using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using crmSeries.Core.Features.Companies.Utility;
using NUnit.Framework;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Companies
{
    [TestFixture]
    public class DeleteCompanyHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_CompanyDeleteFlagSetTrue()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Company.Add(new Company
                {
                    CompanyId = 1,
                    Deleted = false
                });
                context.SaveChanges();

                var handler = new DeleteCompanyHandler(context);

                // Act
                var response = handler.HandleAsync(new DeleteCompanyRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var company = context.Company.SingleOrDefault(x => x.CompanyId == 1);
                Assert.IsNotNull(company);
                Assert.AreEqual(company.Deleted, true);
            }
        }

        [Test]
        public void NormalRequest_NoCompanyMatchingId_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new DeleteCompanyHandler(context);

                // Act
                var response = handler.HandleAsync(new DeleteCompanyRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    CompaniesConstants.ErrorMessages.CompanyNotFound);
            }
        }
    }
}