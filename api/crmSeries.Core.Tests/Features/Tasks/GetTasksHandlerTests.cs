using System.Collections.Generic;
using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks;
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

                var request = new GetTasksRequest { PageNumber = 1, PageSize = 5 };

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

                var request = new GetTasksRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(request);

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

                var request = new GetTasksRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(request);

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

                var request = new GetTasksRequest { PageNumber = 1, PageSize = 5 };

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

        [Test]
        public void HandleAsync_SubjectContainsSearchTerm_ReturnsAppropriateResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var tasks = new List<Domain.HeavyEquipment.Task>
                {
                    new Task { TaskId = 1, Comments = "NA", Subject = "Foobar" },
                    new Task { TaskId = 2, Comments = "NA", Subject = "Barfoo" },
                    new Task { TaskId = 3, Comments = "NA", Subject = "Barfo" },
                    new Task { TaskId = 4, Comments = "NA", Subject = "Fooba" },
                    new Task { TaskId = 5, Comments = "NA", Subject = "Not related" },
                    new Task { TaskId = 6, Comments = "NA", Subject = "Foobar is you" },
                    new Task { TaskId = 7, Comments = "NA", Subject = "I am Foobar" },
                };

                context.Task.AddRange(tasks);
                context.SaveChanges();

                var handler = new GetTasksRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var request = new GetTasksRequest { PageNumber = 1, PageSize = 5, Search = "Foobar" };

                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, 3);
                Assert.IsNotNull(response.Result.Data.Items.Single(x => x.TaskId == 1));
                Assert.IsNotNull(response.Result.Data.Items.Single(x => x.TaskId == 6));
                Assert.IsNotNull(response.Result.Data.Items.Single(x => x.TaskId == 7));
            }
        }
    }
}