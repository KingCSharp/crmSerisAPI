using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.CompanyAssignedCategories;
using crmSeries.Core.Features.CompanyAssignedRanks;
using crmSeries.Core.Features.RelatedRecords;
using NUnit.Framework;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Companies
{
    [TestFixture]
    public class AddCompanyHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_CompanyAddedSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var addCategoryHandler = new AddCompanyAssignedCategoryHandler(context, verificationHandler);
                var addRankHandler = new AddCompanyAssignedRankHandler(context, verificationHandler);
                var handler = new AddCompanyHandler(
                    context, 
                    verificationHandler,
                    addCategoryHandler,
                    addRankHandler);

                // Act
                var response = handler.HandleAsync(new AddCompanyRequest
                {
                    CompanyName = "Name",
                    AccountNo = "000"
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var contact = context.Company.SingleOrDefault(x => x.CompanyId == 1);
                Assert.IsNotNull(contact);
                Assert.AreEqual(contact.CompanyName, "Name");
                Assert.AreEqual(contact.AccountNo, "000");
            }
        }
    }
}