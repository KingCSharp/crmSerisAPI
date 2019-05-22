using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using crmSeries.Core.Logic.Queries;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Companies
{
    [TestFixture]
    public class GetCompaniesFullPagedRequestHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_ReturnsCompaniesFullPagedResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1, Email = "test@email.com" };
                context.User.Add(user);

                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Company.Add(new Company { CompanyId = i });
                    context.CompanyAssignedAddress.Add(
                        new CompanyAssignedAddress { AddressId = i, CompanyId = i });
                    context.Contact.Add(new Contact { ContactId = i, CompanyId = i, Active = true });
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = i,
                        UserId = user.UserId
                    });
                }
                context.SaveChanges();

                var handler = new GetCompaniesFullPagedRequestHandler(
                    context,
                    GetUserContextStub(user.UserId, user.Email));
                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetCompaniesFullPagedRequest
                { 
                    Query = query
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.Items.Count, itemCount / response.Result.Data.PageCount);
                Assert.AreEqual(response.Result.Data.Items[0].Addresses.Count, 1);
                Assert.AreEqual(response.Result.Data.Items[0].Contacts.Count, 1);
                Assert.AreEqual(response.Result.Data.TotalItemCount, itemCount);
                Assert.AreEqual(response.Result.Data.PageNumber, 1);
                Assert.AreEqual(response.Result.Data.PageCount, itemCount / query.PageSize);
                Assert.AreEqual(response.Result.Data.PageNumber, query.PageNumber);
                Assert.AreEqual(response.Result.Data.PageSize, query.PageSize);
            }
        }

        [Test]
        public void NormalRequest_NoCompaniesFound_ReturnsEmptyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1, Email = "test@email.com" };
                context.User.Add(user);
                context.SaveChanges();

                var handler = new GetCompaniesFullPagedRequestHandler(
                    context,
                    GetUserContextStub(user.UserId, user.Email));
                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetCompaniesFullPagedRequest
                {
                    Query = query
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.Items.Count, 0);
                Assert.AreEqual(response.Result.Data.TotalItemCount, 0);
                Assert.AreEqual(response.Result.Data.PageNumber, 1);
                Assert.AreEqual(response.Result.Data.PageCount, 0);
                Assert.AreEqual(response.Result.Data.PageNumber, query.PageNumber);
                Assert.AreEqual(response.Result.Data.PageSize, query.PageSize);
            }
        }
    }
}