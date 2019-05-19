using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Logic.Queries;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Contacts
{
    [TestFixture]
    public class GetContactsRequestHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_ReturnsPagedContactResults()
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
                    context.Contact.Add(new Contact
                    {
                        ContactId = i,
                        CompanyId = i,
                        Active = true,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = i,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = i,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId, user.Email));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query
                });

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
        public void NormalRequest_ContactsInactive_ReturnsEmptyResults()
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
                    context.Contact.Add(new Contact
                    {
                        ContactId = i,
                        CompanyId = i,
                        Active = false,
                        Deleted = false
                    });
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = i,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId, user.Email));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query
                });

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

        [Test]
        public void NormalRequest_ContactsDeleted_ReturnsEmptyResults()
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
                    context.Contact.Add(new Contact
                    {
                        ContactId = i,
                        CompanyId = i,
                        Active = true,
                        Deleted = true
                    });
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = i,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId, user.Email));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query
                });

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

        [Test]
        public void NormalRequest_NoContacts_ReturnsEmptyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1, Email = "test@email.com" };
                context.User.Add(user);
                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId, user.Email));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query
                });

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