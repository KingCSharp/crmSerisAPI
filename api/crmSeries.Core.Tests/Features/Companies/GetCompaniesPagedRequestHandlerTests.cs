using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Companies
{
    [TestFixture]
    public class GetCompaniesPagedRequestHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_ReturnsCompaniesPagedResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);

                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Company.Add(new Company { CompanyId = i });
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = i,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetCompaniesPagedRequestHandler(
                    context, 
                    GetUserContextStub(user.UserId));
                
                var request = new GetCompaniesPagedRequest { PageNumber = 1, PageSize = 5 };
                
                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, itemCount);
                Assert.AreEqual(response.Result.Data.PageNumber, 1);
                Assert.AreEqual(response.Result.Data.PageCount, itemCount / request.PageSize);
                Assert.AreEqual(response.Result.Data.PageNumber, request.PageNumber);
                Assert.AreEqual(response.Result.Data.PageSize, request.PageSize);
            }
        }

        [Test]
        public void NormalRequest_NoCompaniesFound_ReturnsEmptyResult()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var handler = new GetCompaniesPagedRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var request = new GetCompaniesPagedRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, 0);
                Assert.AreEqual(response.Result.Data.PageNumber, 1);
                Assert.AreEqual(response.Result.Data.PageCount, 0);
                Assert.AreEqual(response.Result.Data.PageNumber, request.PageNumber);
                Assert.AreEqual(response.Result.Data.PageSize, request.PageSize);
            }
        }
    }
}