using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks;
using crmSeries.Core.Logic.Queries;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Tasks
{
    [TestFixture]
    public class GetTasksHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssues_ReturnsTaskResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Task.Add(new Task
                    {
                        TaskId = i,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetTasksRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetTasksRequest { PageInfo = query });

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
        public void HandleAsync_HasCompanyAsRelatedRecord_ReturnsTasksWithRelatedRecordNameSetToCompanyName()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);

                var company = new Company
                {
                    CompanyId = 1,
                    CompanyName = "Nubitz, LLC"
                };

                context.Company.Add(company);
                context.SaveChanges();

                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Task.Add(new Task
                    {
                        TaskId = i,
                        UserId = user.UserId,
                        RelatedRecordId = company.CompanyId,
                        RelatedRecordType = Constants.RelatedRecord.Types.Company
                    });
                }

                context.SaveChanges();

                var handler = new GetTasksRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetTasksRequest { PageInfo = query });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                foreach (var task in response.Result.Data.Items)
                {
                    Assert.AreEqual(company.CompanyName, task.RelatedRecordName);
                }
            }
        }

        [Test]
        public void HandleAsync_HasContactAsRelatedRecord_ReturnsTasksWithRelatedRecordNameSetToContactFirstNameAndLastNameConcatenated()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);

                var contact = new Contact
                {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Active = true
                };

                context.Contact.Add(contact);
                context.SaveChanges();

                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Task.Add(new Task
                    {
                        TaskId = i,
                        UserId = user.UserId,
                        RelatedRecordId = contact.ContactId,
                        RelatedRecordType = Constants.RelatedRecord.Types.Contact
                    });
                }
                context.SaveChanges();

                var handler = new GetTasksRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetTasksRequest { PageInfo = query });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                foreach (var task in response.Result.Data.Items)
                {
                    Assert.AreEqual($"{contact.FirstName} {contact.LastName}", task.RelatedRecordName);
                }
            }
        }

        [Test]
        public void HandleAsync_HasLeadAsRelatedRecord_ReturnsTasksWithRelatedRecordNameSetToLeadFirstNameAndLastNameConcatenated()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);

                var lead = new Lead
                {
                    LeadId = 1,
                    FirstName = "John",
                    LastName = "Doe"
                };

                context.Lead.Add(lead);
                context.SaveChanges();

                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Task.Add(new Task
                    {
                        TaskId = i,
                        UserId = user.UserId,
                        RelatedRecordId = lead.LeadId,
                        RelatedRecordType = Constants.RelatedRecord.Types.Lead
                    });
                }
                context.SaveChanges();

                var handler = new GetTasksRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetTasksRequest { PageInfo = query });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                foreach (var task in response.Result.Data.Items)
                {
                    Assert.AreEqual($"{lead.FirstName} {lead.LastName}", task.RelatedRecordName);
                }
            }
        }

        [Test]
        public void HandleAsync_NoTasksFound_ReturnsEmptyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var handler = new GetTasksRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetTasksRequest { PageInfo = query });

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