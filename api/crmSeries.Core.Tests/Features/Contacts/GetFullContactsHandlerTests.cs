using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Logic.Queries;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Contacts
{
    [TestFixture]
    public class GetFullContactsHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssues_ReturnsPagedContactResults()
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

                var handler = new GetFullContactsHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetFullContactsRequest
                {
                    PageInfo = query
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(itemCount, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(itemCount / query.PageSize, response.Result.Data.PageCount);
                Assert.AreEqual(query.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(query.PageSize, response.Result.Data.PageSize);
            }
        }

        [Test]
        public void HandleAsync_NoIssues_ReturnsContactsWithEmptyRelatedRecords()
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

                var handler = new GetFullContactsHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetFullContactsRequest
                {
                    PageInfo = query
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(itemCount, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(itemCount / query.PageSize, response.Result.Data.PageCount);
                Assert.AreEqual(query.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(query.PageSize, response.Result.Data.PageSize);
                Assert.AreEqual(0, response.Result.Data.Items.Where(x => x.Notes.Count() > 0).Count());
                Assert.AreEqual(0, response.Result.Data.Items.Where(x => x.Tasks.Count() > 0).Count());
            }
        }

        [Test]
        public void HandleAsync_NoIssues_ReturnsContactsWithRelatedNotes()
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

                    context.Note.Add(new Note
                    {
                        NoteId = i,
                        RecordId = i,
                        RecordType = Constants.RelatedRecord.Types.Contact
                    });
                }

                context.SaveChanges();

                var handler = new GetFullContactsHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetFullContactsRequest
                {
                    PageInfo = query
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(itemCount, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(itemCount / query.PageSize, response.Result.Data.PageCount);
                Assert.AreEqual(query.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(query.PageSize, response.Result.Data.PageSize);
                Assert.AreEqual(query.PageSize, response.Result.Data.Items.Where(x => x.Notes.Count() > 0).Count());
                Assert.AreEqual(0, response.Result.Data.Items.Where(x => x.Tasks.Count() > 0).Count());
            }
        }

        [Test]
        public void HandleAsync_NoIssues_ReturnsContactsWithRelatedTasks()
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

                    context.Task.Add(new Task
                    {
                        TaskId = i,
                        ContactId = i,
                    });
                }

                context.SaveChanges();

                var handler = new GetFullContactsHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetFullContactsRequest
                {
                    PageInfo = query
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(itemCount, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(itemCount / query.PageSize, response.Result.Data.PageCount);
                Assert.AreEqual(query.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(query.PageSize, response.Result.Data.PageSize);
                Assert.AreEqual(0, response.Result.Data.Items.Where(x => x.Notes.Count() > 0).Count());
                Assert.AreEqual(query.PageSize, response.Result.Data.Items.Where(x => x.Tasks.Count() > 0).Count());
            }
        }

        [Test]
        public void HandleAsync_ContactsInactive_ReturnsEmptyResults()
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

                var handler = new GetFullContactsHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetFullContactsRequest
                {
                    PageInfo = query
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(0, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(0, response.Result.Data.PageCount);
                Assert.AreEqual(query.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(query.PageSize, response.Result.Data.PageSize);
            }
        }

        [Test]
        public void HandleAsync_ContactsDeleted_ReturnsEmptyResults()
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

                var handler = new GetFullContactsHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetFullContactsRequest
                {
                    PageInfo = query
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(0, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(0, response.Result.Data.PageCount);
                Assert.AreEqual(query.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(query.PageSize, response.Result.Data.PageSize);
            }
        }

        [Test]
        public void HandleAsync_NoContacts_ReturnsEmptyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var handler = new GetFullContactsHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetFullContactsRequest
                {
                    PageInfo = query
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(0, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(0, response.Result.Data.PageCount);
                Assert.AreEqual(query.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(query.PageSize, response.Result.Data.PageSize);
            }
        }
    }
}