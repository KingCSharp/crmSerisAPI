using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Equipment;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Security;
using NUnit.Framework;
using System.Collections.Generic;

namespace crmSeries.Core.Tests.Features.Equipment
{
    [TestFixture]
    public class GetEquipmentHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_ReturnsEquipmentPagedResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Equipment.Add(new Domain.HeavyEquipment.Equipment
                    {
                        EquipmentId = i,
                        NewUsed = "New",
                        Deleted = false,
                        Inventory = true,
                        AvailableForQuote = true
                    });
                }

                context.SaveChanges();

                var handler = new GetEquipmentHandler(context);

                var pageInfo = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetEquipmentRequest
                {
                    PageInfo = pageInfo
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(itemCount, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(itemCount / pageInfo.PageSize, response.Result.Data.PageCount);
                Assert.AreEqual(pageInfo.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(pageInfo.PageSize, response.Result.Data.PageSize);
            }
        }

        [Test]
        public void NormalRequest_NoIssues_ReturnsEquipmentPagedResultsWithBranchAndCategory()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                int itemCount = 10;

                context.Branch.Add(new Branch
                {
                    BranchId = 1,
                    BranchName = "Test Branch"
                });

                context.EquipmentCategory.Add(new EquipmentCategory
                {
                    CategoryId = 1,
                    Category = "Test Category"
                });

                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Equipment.Add(new Domain.HeavyEquipment.Equipment
                    {
                        EquipmentId = i,
                        CategoryId = 1,
                        CurrentBranchId = 1,
                        NewUsed = "New",
                        Deleted = false,
                        Inventory = true,
                        AvailableForQuote = true
                    });
                }

                context.SaveChanges();

                var handler = new GetEquipmentHandler(context);

                var pageInfo = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetEquipmentRequest
                {
                    PageInfo = pageInfo,
                    BranchId = 1
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(itemCount, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(itemCount / pageInfo.PageSize, response.Result.Data.PageCount);
                Assert.AreEqual(pageInfo.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(pageInfo.PageSize, response.Result.Data.PageSize);
                Assert.AreEqual("Test Branch", response.Result.Data.Items[0].BranchName);
                Assert.AreEqual("Test Category", response.Result.Data.Items[0].Category);
            }
        }

        [Test]
        public void NormalRequest_NoIssues_ReturnsEquipmentPagedResultsWithCorrectEquipmentType()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                int itemCount = 10;

                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Equipment.Add(new Domain.HeavyEquipment.Equipment
                    {
                        EquipmentId = i,
                        CategoryId = 1,
                        CurrentBranchId = 1,
                        NewUsed = "New",
                        Deleted = false,
                        Inventory = true,
                        AvailableForQuote = true
                    });
                }

                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Equipment.Add(new Domain.HeavyEquipment.Equipment
                    {
                        EquipmentId = i + itemCount,
                        CategoryId = 1,
                        CurrentBranchId = 1,
                        NewUsed = "Used",
                        Deleted = false,
                        Inventory = true,
                        AvailableForQuote = true
                    });
                }

                context.SaveChanges();

                var handler = new GetEquipmentHandler(context);

                var pageInfo = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetEquipmentRequest
                {
                    PageInfo = pageInfo,
                    EquipmentType = "New"
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(itemCount, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(itemCount / pageInfo.PageSize, response.Result.Data.PageCount);
                Assert.AreEqual(pageInfo.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(pageInfo.PageSize, response.Result.Data.PageSize);
            }
        }

        [Test]
        public void NormalRequest_NoIssues_ReturnsEquipmentPagedResultsWithCorrectStatus()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                int itemCount = 10;

                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Equipment.Add(new Domain.HeavyEquipment.Equipment
                    {
                        EquipmentId = i,
                        CategoryId = 1,
                        CurrentBranchId = 1,
                        Deleted = false,
                        Inventory = true,
                        AvailableForQuote = true,
                        Status = "Testing"
                    });
                }

                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Equipment.Add(new Domain.HeavyEquipment.Equipment
                    {
                        EquipmentId = i + itemCount,
                        CategoryId = 1,
                        CurrentBranchId = 1,
                        Deleted = false,
                        Inventory = true,
                        AvailableForQuote = true,
                        Status = "Testing 2"
                    });
                }

                context.SaveChanges();

                var handler = new GetEquipmentHandler(context);

                var pageInfo = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetEquipmentRequest
                {
                    PageInfo = pageInfo,
                    Statuses = new List<string>
                    {
                        "Testing"
                    }
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(itemCount, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(itemCount / pageInfo.PageSize, response.Result.Data.PageCount);
                Assert.AreEqual(pageInfo.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(pageInfo.PageSize, response.Result.Data.PageSize);
            }
        }

        [Test]
        public void NormalRequest_NoIssues_ReturnsEquipmentPagedResultsWithCorrectStatuses()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                int itemCount = 10;

                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Equipment.Add(new Domain.HeavyEquipment.Equipment
                    {
                        EquipmentId = i,
                        CategoryId = 1,
                        CurrentBranchId = 1,
                        Deleted = false,
                        Inventory = true,
                        AvailableForQuote = true,
                        Status = "Testing"
                    });
                }

                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Equipment.Add(new Domain.HeavyEquipment.Equipment
                    {
                        EquipmentId = i + itemCount,
                        CategoryId = 1,
                        CurrentBranchId = 1,
                        Deleted = false,
                        Inventory = true,
                        AvailableForQuote = true,
                        Status = "Testing 2"
                    });
                }

                context.SaveChanges();

                var handler = new GetEquipmentHandler(context);

                var pageInfo = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetEquipmentRequest
                {
                    PageInfo = pageInfo,
                    Statuses = new List<string>
                    {
                        "Testing",
                        "Testing 2"
                    }
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(itemCount * 2, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual((itemCount * 2) / pageInfo.PageSize, response.Result.Data.PageCount);
                Assert.AreEqual(pageInfo.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(pageInfo.PageSize, response.Result.Data.PageSize);
            }
        }

        [Test]
        public void NormalRequest_NoEquipmentFound_ReturnsEmptyResult()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {                
                var handler = new GetEquipmentHandler(context);

                var pageInfo = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetEquipmentRequest
                {
                    PageInfo = pageInfo
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(0, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(0, response.Result.Data.PageCount);
                Assert.AreEqual(pageInfo.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(pageInfo.PageSize, response.Result.Data.PageSize);
            }
        }

        [Test]
        public void NormalRequest_NoEquipmentWithBranch_ReturnsEmptyResult()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Equipment.Add(new Domain.HeavyEquipment.Equipment
                    {
                        EquipmentId = i,
                        NewUsed = "New",
                        Deleted = false,
                        Inventory = true,
                        AvailableForQuote = true
                    });
                }

                context.SaveChanges();

                var handler = new GetEquipmentHandler(context);

                var pageInfo = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetEquipmentRequest
                {
                    PageInfo = pageInfo,
                    BranchId = 1
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(0, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(0, response.Result.Data.PageCount);
                Assert.AreEqual(pageInfo.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(pageInfo.PageSize, response.Result.Data.PageSize);
            }
        }

        [Test]
        public void NormalRequest_NoEquipmentWithEquipmentType_ReturnsEmptyResult()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Equipment.Add(new Domain.HeavyEquipment.Equipment
                    {
                        EquipmentId = i,
                        NewUsed = "New",
                        Deleted = false,
                        Inventory = true,
                        AvailableForQuote = true
                    });
                }

                context.SaveChanges();

                var handler = new GetEquipmentHandler(context);

                var pageInfo = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetEquipmentRequest
                {
                    PageInfo = pageInfo,
                    EquipmentType = "Foo"
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(0, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(0, response.Result.Data.PageCount);
                Assert.AreEqual(pageInfo.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(pageInfo.PageSize, response.Result.Data.PageSize);
            }
        }

        [Test]
        public void NormalRequest_NoEquipmentWithStatuses_ReturnsEmptyResult()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Equipment.Add(new Domain.HeavyEquipment.Equipment
                    {
                        EquipmentId = i,
                        NewUsed = "New",
                        Deleted = false,
                        Inventory = true,
                        AvailableForQuote = true
                    });
                }

                context.SaveChanges();

                var handler = new GetEquipmentHandler(context);

                var pageInfo = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetEquipmentRequest
                {
                    PageInfo = pageInfo,
                    Statuses = new List<string>
                    {
                        "Foo",
                        "Bar"
                    }
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(0, response.Result.Data.TotalItemCount);
                Assert.AreEqual(1, response.Result.Data.PageNumber);
                Assert.AreEqual(0, response.Result.Data.PageCount);
                Assert.AreEqual(pageInfo.PageNumber, response.Result.Data.PageNumber);
                Assert.AreEqual(pageInfo.PageSize, response.Result.Data.PageSize);
            }
        }
    }
}