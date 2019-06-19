using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Logic.Queries;
using NUnit.Framework;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Contacts
{
    [TestFixture]
    public class GetContactsRequestHandlerTests : BaseUnitTest
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

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

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

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

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

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

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
        public void HandleAsync_NoContacts_ReturnsEmptyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

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
        public void HandleAsync_ActiveOptionsSetToActiveOnly_ReturnsOnlyActiveContacts()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var entityId = 1;

                var activeContactsCount = 15;
                for (int i = 0; i < activeContactsCount; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        Active = true,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                var deactiveContactsCount = 3;
                for (int i = 0; i < deactiveContactsCount; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        Active = false,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 10 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query,
                    ActiveOptions = ActiveOptions.ActiveOnly
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(activeContactsCount, response.Result.Data.TotalItemCount);
                Assert.IsTrue(activeContactsCount != deactiveContactsCount);
            }
        }

        [Test]
        public void HandleAsync_ActiveOptionsSetToDeactiveOnly_ReturnsOnlyDeactiveContacts()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var entityId = 1;

                var activeContactsCount = 5;
                for (int i = 0; i < activeContactsCount; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        Active = true,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                var deactiveContactsCount = 15;

                for (int i = 0; i < deactiveContactsCount; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        Active = false,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 10 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query,
                    ActiveOptions = ActiveOptions.DeactiveOnly
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(deactiveContactsCount, response.Result.Data.TotalItemCount);
            }
        }

        [Test]
        public void HandleAsync_ActiveOptionsSetToAll_ReturnsActiveAndDeactiveContacts()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var entityId = 1;

                var activeContactsCount = 5;

                for (int i = 0; i < 5; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        Active = true,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                var deactiveContactsCount = 15;

                for (int i = 0; i < deactiveContactsCount; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        Active = false,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 10 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query,
                    ActiveOptions = ActiveOptions.All
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(activeContactsCount + deactiveContactsCount, response.Result.Data.TotalItemCount);
            }
        }

        [Test]
        public void HandleAsync_ActiveOptionsNotPartOfRequestObject_ReturnsOnlyActiveContacts()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var entityId = 1;

                var activeContactsCount = 5;

                for (int i = 0; i < 5; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        Active = true,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                var deactiveContactsCount = 15;

                for (int i = 0; i < deactiveContactsCount; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        Active = false,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 10 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(activeContactsCount, response.Result.Data.TotalItemCount);
                Assert.IsTrue(activeContactsCount != deactiveContactsCount);
            }
        }

        [Test]
        public void HandleAsync_FirstNameIncluded_ReturnsOnlyContactsWhoseFirstNameStartsWithValue()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var entityId = 1;

                var firstNameRequest = "Jo";
                var names = new[] { "Jo", "Joseph", "Sammy", "Joooo", "JJ" };
                var namesStartingWithRequestCount = names.Count(x => x.StartsWith(firstNameRequest));

                for (int i = 0; i < 5; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        FirstName = names[i],
                        Active = true,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 10 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query,
                    FirstName = firstNameRequest,
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(namesStartingWithRequestCount, response.Result.Data.TotalItemCount);
            }
        }

        [Test]
        public void HandleAsync_FirstNameIncludedDifferentCase_IgnoreCaseAndReturnsOnlyContactsWhoseFirstNameStartsWithValue()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var entityId = 1;

                var firstNameRequest = "Jo";
                var names = new[] { "Jo", "Joseph", "Sammy", "Joooo", "JJ" };
                var namesStartingWithRequestCount = names.Count(x => x.StartsWith(firstNameRequest));

                for (int i = 0; i < 5; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        FirstName = names[i].ToLower(),
                        Active = true,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 10 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query,
                    FirstName = firstNameRequest,
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(namesStartingWithRequestCount, response.Result.Data.TotalItemCount);
            }
        }

        [Test]
        public void HandleAsync_LastNameIncluded_ReturnsOnlyContactsWhoseLastNameStartsWithValue()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var entityId = 1;

                var lastNameRequest = "Jo";
                var names = new[] { "Jo", "Joseph", "Sammy", "Joooo", "JJ" };
                var namesStartingWithRequestCount = names.Count(x => x.StartsWith(lastNameRequest));

                for (int i = 0; i < 5; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        LastName = names[i],
                        Active = true,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 10 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query,
                    LastName = lastNameRequest,
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(namesStartingWithRequestCount, response.Result.Data.TotalItemCount);
            }
        }

        [Test]
        public void HandleAsync_LastNameIncludedDifferentCase_IgnoreCaseAndReturnsOnlyContactsWhoseLastNameStartsWithValue()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var entityId = 1;

                var lastNameRequest = "Jo";
                var names = new[] { "Jo", "Joseph", "Sammy", "Joooo", "JJ" };
                var namesStartingWithRequestCount = names.Count(x => x.StartsWith(lastNameRequest));

                for (int i = 0; i < 5; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = entityId,
                        FirstName = names[i].ToLower(),
                        Active = true,
                        Deleted = false
                    });

                    var company = new Company
                    {
                        CompanyId = entityId,
                        CompanyName = "Foo Company"
                    };

                    context.Company.Add(company);
                    context.CompanyAssignedUser.Add(new CompanyAssignedUser
                    {
                        CompanyId = entityId,
                        UserId = user.UserId
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 10 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageInfo = query,
                    FirstName = lastNameRequest,
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(namesStartingWithRequestCount, response.Result.Data.TotalItemCount);
            }
        }
    }
}