using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using NUnit.Framework;
using System.Linq;

namespace crmSeries.Unit.Tests.Companies
{
    [TestFixture]
    public class GetCompaniesFullRequestHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_ReturnsFullCompanyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions(
                "NormalRequest_NoIssues_ReturnsFullCompanyResults");

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
                    context.Contact.Add(new Contact { ContactId = i, CompanyId = i });
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = i,
                        UserId = user.UserId
                    });
                }
                context.SaveChanges();

                var handler = new GetCompaniesFullRequestHandler(
                    context,
                    GetUserContextStub(user.UserId, user.Email));

                // Act
                var response = handler.HandleAsync(new GetCompaniesFullRequest());

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.Count(), itemCount);
            }
        }

        [Test]
        public void NormalRequest_NoCompaniesFullFound_ReturnsEmptyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions(
                "NormalRequest_NoCompaniesFullFound_ReturnsEmptyResults");

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1, Email = "test@email.com" };
                context.User.Add(user);
                context.SaveChanges();

                var handler = new GetCompaniesFullRequestHandler(
                    context,
                    GetUserContextStub(user.UserId, user.Email));

                // Act
                var response = handler.HandleAsync(new GetCompaniesFullRequest());

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.Count(), 0);
            }
        }
    }
}