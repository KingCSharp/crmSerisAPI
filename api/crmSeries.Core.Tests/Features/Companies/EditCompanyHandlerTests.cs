using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using crmSeries.Core.Features.Companies.Utility;
using NUnit.Framework;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Companies
{
    [TestFixture]
    public class EditCompanyHandlerTests : BaseUnitTest
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
                    CompanyName = "Test Name",
                    AccountNo = "000",
                    Deleted = false
                });
                context.SaveChanges();
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new EditCompanyHandler(context);

                // Act
                var response = handler.HandleAsync(new EditCompanyRequest
                {
                    CompanyId = 1,
                    CompanyName = "Edited Name",
                    AccountNo = "111"
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var company = context.Company.SingleOrDefault(x => x.CompanyId == 1);
                Assert.IsNotNull(company);
                Assert.AreEqual(company.CompanyName, "Edited Name");
                Assert.AreEqual(company.AccountNo, "111");
            }
        }

        [Test]
        public void NormalRequest_NoCompanyMatchingId_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new EditCompanyHandler(context);

                // Act
                var response = handler.HandleAsync(new EditCompanyRequest
                {
                    CompanyId = 1,
                    CompanyName = "Edited Name",
                    AccountNo = "111"
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    CompaniesConstants.ErrorMessages.CompanyNotFound);
            }
        }
    }
}