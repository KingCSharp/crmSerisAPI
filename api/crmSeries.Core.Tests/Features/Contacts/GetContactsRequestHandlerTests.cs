using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
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

                var request = new GetContactsRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetContactsRequest
                {
                    PageNumber = 1,
                    PageSize = 5
                });

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

                var request = new GetContactsRequest { PageNumber = 1, PageSize = 5 };

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

                var request = new GetContactsRequest { PageNumber = 1, PageSize = 5 };

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

                var request = new GetContactsRequest { PageNumber = 1, PageSize = 5 };

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

                var inactiveContactsCount = 3;
                for (int i = 0; i < inactiveContactsCount; ++i, entityId++)
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

                var request = new GetContactsRequest { PageNumber = 1, PageSize = 10 };

                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(activeContactsCount, response.Result.Data.TotalItemCount);
                Assert.IsTrue(activeContactsCount != inactiveContactsCount);
            }
        }

        [Test]
        public void HandleAsync_ActiveOptionsSetToInactiveOnly_ReturnsOnlyInactiveContacts()
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

                var inactiveContactsCount = 15;

                for (int i = 0; i < inactiveContactsCount; ++i, entityId++)
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

                var request = new GetContactsRequest
                {
                    PageNumber = 1,
                    PageSize = 10,
                    ActiveOptions = ActiveOptions.InactiveOnly
                };

                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(inactiveContactsCount, response.Result.Data.TotalItemCount);
            }
        }

        [Test]
        public void HandleAsync_ActiveOptionsSetToAll_ReturnsActiveAndInactiveContacts()
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

                var inactiveContactsCount = 15;

                for (int i = 0; i < inactiveContactsCount; ++i, entityId++)
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

                var request = new GetContactsRequest
                {
                    PageNumber = 1,
                    PageSize = 10,
                    ActiveOptions = ActiveOptions.All
                };

                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(activeContactsCount + inactiveContactsCount, response.Result.Data.TotalItemCount);
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

                var inactiveContactsCount = 15;

                for (int i = 0; i < inactiveContactsCount; ++i, entityId++)
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

                var request = new GetContactsRequest
                {
                    PageNumber = 1,
                    PageSize = 10
                };

                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(activeContactsCount, response.Result.Data.TotalItemCount);
                Assert.IsTrue(activeContactsCount != inactiveContactsCount);
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
                        LastName = "",
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

                var request = new GetContactsRequest
                {
                    PageNumber = 1,
                    PageSize = 10,
                    Search = firstNameRequest
                };

                // Act
                var response = handler.HandleAsync(request);

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
                        LastName = "",
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

                var request = new GetContactsRequest
                {
                    PageNumber = 1,
                    PageSize = 10,
                    Search = firstNameRequest
                };

                // Act
                var response = handler.HandleAsync(request);

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
                        FirstName = "",
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

                var request = new GetContactsRequest
                {
                    PageNumber = 1,
                    PageSize = 10,
                    Search = lastNameRequest
                };

                // Act
                var response = handler.HandleAsync(request);

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
                        LastName = "",
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

                var request = new GetContactsRequest
                {
                    PageNumber = 1,
                    PageSize = 10,
                    Search = lastNameRequest
                };

                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(namesStartingWithRequestCount, response.Result.Data.TotalItemCount);
            }
        }

        [Test]
        public void HandleAsync_CompanyIdIncluded_ReturnsOnlyContactsWhoseAreAssociatedWithCompany()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var entityId = 1;
                var companyId = 33;
                
                var company = new Company
                {
                    CompanyId = companyId,
                    CompanyName = "Foo Company"
                };

                context.Company.Add(company);
                context.CompanyAssignedUser.Add(new CompanyAssignedUser
                {
                    CompanyId = companyId,
                    UserId = user.UserId
                });

                context.SaveChanges();

                var contactsMatchingCompany = 5;
                for (int i = 0; i < contactsMatchingCompany; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = companyId,
                        Active = true,
                        Deleted = false
                    });
                }

                for (int i = 0; i < 7; ++i, entityId++)
                {
                    context.Contact.Add(new Contact
                    {
                        ContactId = entityId,
                        CompanyId = companyId + 1,
                        Active = true,
                        Deleted = false
                    });
                }

                context.SaveChanges();

                var handler = new GetContactsRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var request = new GetContactsRequest
                {
                    PageNumber = 1,
                    PageSize = 10,
                    CompanyId = companyId
                };

                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(contactsMatchingCompany, response.Result.Data.TotalItemCount);
                Assert.IsTrue(response.Result.Data.Items.All(x => x.CompanyId == companyId));
            }
        }
    }
}