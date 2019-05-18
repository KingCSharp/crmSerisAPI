using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Security;
using NUnit.Framework;


namespace crmSeries.Unit.Tests.Companies
{
    [TestFixture]
    public class GetAllCompaniesPagedRequestHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_ReturnsPagedCompanyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions(
                "NormalRequest_NoIssues_ReturnsPagedCompanyResults");

            using (var context = new HeavyEquipmentContext(options))
            {
                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Company.Add(new Company { CompanyId = i });
                }

                context.SaveChanges();

                var handler = new GetAllCompaniesPagedRequestHandler(context);

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetAllCompaniesPagedRequest { Query = query });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, itemCount);
                Assert.AreEqual(response.Result.Data.PageNumber, 1);
                Assert.AreEqual(response.Result.Data.PageCount, itemCount / query.PageSize);
                Assert.AreEqual(response.Result.Data.PageNumber, query.PageNumber);
                Assert.AreEqual(response.Result.Data.PageSize, query.PageSize);
            }
        }

        [Test]
        public void NormalRequest_NoCompaniesFound_ReturnsEmptyData()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions(
                "NormalRequest_NoCompaniesFound_ReturnsEmptyData");

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new GetAllCompaniesPagedRequestHandler(context);
                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetAllCompaniesPagedRequest { Query = query });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, 0);
                Assert.AreEqual(response.Result.Data.PageNumber, 1);
                Assert.AreEqual(response.Result.Data.PageCount, 0);
                Assert.AreEqual(response.Result.Data.PageNumber, query.PageNumber);
                Assert.AreEqual(response.Result.Data.PageSize, query.PageSize);
            }
        }
    }
}