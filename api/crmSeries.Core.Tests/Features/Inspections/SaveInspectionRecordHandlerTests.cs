using System;
using System.Collections.Generic;
using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Inspections;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Features.Inspections.Utility;
using crmSeries.Core.Features.Inventory.Utility;
using crmSeries.Core.Features.RelatedRecords;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;

namespace crmSeries.Core.Tests.Features.Inspections
{
    public class SaveInspectionRecordHandlerTests : BaseUnitTest
    {
        private SaveInspectionRecordRequest _request;

        [SetUp]
        public void TestSetUp()
        {
            var item1ResponseDtos = new List<RecordAssignedInspectionItemResponseDto>
            {
                new RecordAssignedInspectionItemResponseDto
                {
                    Response = "Response 1",
                    Sequence = 1
                },
                new RecordAssignedInspectionItemResponseDto
                {
                    Response = "Response 2",
                    Sequence = 2
                }
            };

            var item2ResponseDtos = new List<RecordAssignedInspectionItemResponseDto>
            {
                new RecordAssignedInspectionItemResponseDto
                {
                    Response = "Response 3",
                    Sequence = 1
                },
                new RecordAssignedInspectionItemResponseDto
                {
                    Response = "Response 4",
                    Sequence = 2
                }
            };

            var item3ResponseDtos = new List<RecordAssignedInspectionItemResponseDto>
            {
                new RecordAssignedInspectionItemResponseDto
                {
                    Response = "Response 5",
                    Sequence = 1
                },
                new RecordAssignedInspectionItemResponseDto
                {
                    Response = "Response 6",
                    Sequence = 2
                }
            };

            var item4ResponseDtos = new List<RecordAssignedInspectionItemResponseDto>
            {
                new RecordAssignedInspectionItemResponseDto
                {
                    Response = "Response 7",
                    Sequence = 1
                },
                new RecordAssignedInspectionItemResponseDto
                {
                    Response = "Response 8",
                    Sequence = 2
                }
            };

            var group1ItemDtos = new List<RecordAssignedInspectionItemDto>
            {
                new RecordAssignedInspectionItemDto
                {
                    Comments = "Comments 1",
                    DataType = "Type",
                    Item = "Item 1",
                    Response = "Response 1",
                    Sequence = 1,
                    Responses = item1ResponseDtos
                },
                new RecordAssignedInspectionItemDto
                {
                    Comments = "Comments 2",
                    DataType = "Type",
                    Item = "Item 2",
                    Response = "Response 2",
                    Sequence = 2,
                    Responses = item2ResponseDtos
                }
            };

            var group2ItemDtos = new List<RecordAssignedInspectionItemDto>
            {
                new RecordAssignedInspectionItemDto
                {
                    Comments = "Comments 3",
                    DataType = "Type",
                    Item = "Item 3",
                    Response = "Response 3",
                    Sequence = 1,
                    Responses = item3ResponseDtos
                },
                new RecordAssignedInspectionItemDto
                {
                    Comments = "Comments 4",
                    DataType = "Type",
                    Item = "Item 4",
                    Response = "Response 4",
                    Sequence = 2,
                    Responses = item4ResponseDtos
                }
            };

            var groupDtos = new List<RecordAssignedInspectionGroupDto>
            {
                new RecordAssignedInspectionGroupDto
                {
                    Comments = "Comments 1",
                    GroupName = "Group 1",
                    Sequence = 1,
                    Items = group1ItemDtos
                },
                new RecordAssignedInspectionGroupDto
                {
                    Comments = "Comments 2",
                    GroupName = "Group 2",
                    Sequence = 2,
                    Items = group2ItemDtos
                }
            };

            var inspectionDto = new RecordAssignedInspectionDto
            {
                Comments = "Comments",
                InspectionDate = DateTime.UtcNow,
                InspectionHours = 0,
                InspectionId = 1,
                InspectionName = "Inspection 1",
                InspectionType = "Type",
                RecordId = 1,
                RecordType = "Equipment",
                Groups = groupDtos
            };

            _request = new SaveInspectionRecordRequest(inspectionDto);
        }

        private Task CreateRelatedRecords(HeavyEquipmentContext context)
        {
            if (_request.Inspection.Inspection.InspectionId > 0)
                context.Add(new Inspection { InspectionId = _request.Inspection.Inspection.InspectionId });

            if (_request.Inspection.Inspection.RecordId > 0)
                context.Add(new Domain.HeavyEquipment.Equipment { EquipmentId = _request.Inspection.Inspection.RecordId });

            if (_request.Inspection.AssignedInspectionId.HasValue)
                context.Add(new RecordAssignedInspection { AssignedInspectionId = _request.Inspection.AssignedInspectionId.Value });

            return context.SaveChangesAsync();
        }

        [Test]
        public async Task HandleAsync_WithValidRequest_ShouldCreateRecords()
        {
            // Arrange
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                await CreateRelatedRecords(context);

                var handler = new SaveInspectionRecordHandler(context, new VerifyRelatedRecordHandler(context));

                // Act
                var response = await handler.HandleAsync(_request);

                // Assert
                Assert.AreEqual(false, response.HasErrors);
                Assert.NotNull(response.Data);
                Assert.AreEqual(1, await context.Set<RecordAssignedInspection>().CountAsync());
                Assert.NotZero(response.Data.AssignedInspectionId);
                Assert.AreEqual(response.Data.AssignedInspectionId, (await context.Set<RecordAssignedInspection>().SingleAsync()).AssignedInspectionId);
            }
        }

        [Test]
        public async Task HandleAsync_WithBadRelatedRecords_ShouldReturnError()
        {
            // Arrange
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new SaveInspectionRecordHandler(context, new VerifyRelatedRecordHandler(context));

                // Act
                var response = await handler.HandleAsync(_request);

                // Assert
                Assert.AreEqual(true, response.HasErrors);
                Assert.AreEqual(InventoryConstants.ErrorMessages.InventoryNotFound, response.Errors[0].ErrorMessage);
            }
        }

        [Test]
        public async Task HandleAsync_WithBadAssignedInspectionId_ShouldReturnError()
        {
            // Arrange
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new SaveInspectionRecordHandler(context, new VerifyRelatedRecordHandler(context));

                _request.Inspection.AssignedInspectionId = null;
                await CreateRelatedRecords(context);
                _request.Inspection.AssignedInspectionId = 1;

                // Act
                var response = await handler.HandleAsync(_request);

                // Assert
                Assert.AreEqual(true, response.HasErrors);
                Assert.AreEqual(InspectionConstants.ErrorMessages.InspectionRecordNotFound, response.Errors[0].ErrorMessage);
            }
        }

        [Test]
        public async Task HandleAsync_WithAssignedInspectionId_ShouldDeleteOldInspectionAndCreateNewInspection()
        {
            // Arrange
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new SaveInspectionRecordHandler(context, new VerifyRelatedRecordHandler(context));

                _request.Inspection.AssignedInspectionId = 10;
                await CreateRelatedRecords(context);
                
                // Act
                var response = await handler.HandleAsync(_request);

                // Assert
                Assert.AreEqual(false, response.HasErrors);
                Assert.AreEqual(2, await context.Set<RecordAssignedInspection>().CountAsync());

                var oldInspection = await context.Set<RecordAssignedInspection>()
                    .SingleAsync(x => x.AssignedInspectionId == _request.Inspection.AssignedInspectionId);

                Assert.AreNotEqual(oldInspection.AssignedInspectionId, response.Data.AssignedInspectionId);
                Assert.IsTrue(oldInspection.Deleted);
            }
        }

        [Test]
        public async Task HandleAsync_WithValidRequest_ShouldPreserveHierarchy()
        {
            // Arrange
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new SaveInspectionRecordHandler(context, new VerifyRelatedRecordHandler(context));
                
                await CreateRelatedRecords(context);

                // Act
                var response = await handler.HandleAsync(_request);

                // Assert
                Assert.AreEqual(false, response.HasErrors);

                var request = _request.Inspection.Inspection;
                var result = response.Data;

                Assert.AreEqual(request.InspectionName, result.InspectionName);
                Assert.AreEqual(request.Groups.Count, result.Groups.Count);

                for (int g=0; g<request.Groups.Count; ++g)
                {
                    Assert.AreEqual(request.Groups[g].GroupName, result.Groups[g].GroupName);
                    Assert.AreEqual(request.Groups[g].Items.Count, result.Groups[g].Items.Count);

                    for (int i=0; i<request.Groups[g].Items.Count; ++i)
                    {
                        Assert.AreEqual(request.Groups[g].Items[i].Item, result.Groups[g].Items[i].Item);
                        Assert.AreEqual(request.Groups[g].Items[i].Responses.Count, result.Groups[g].Items[i].Responses.Count);

                        for (int r = 0; r < request.Groups[g].Items[i].Responses.Count; ++r)
                            Assert.AreEqual(request.Groups[g].Items[i].Responses[r].Response, result.Groups[g].Items[i].Responses[r].Response);
                    }
                }

                Assert.AreEqual(1, await context.Set<RecordAssignedInspection>().CountAsync());
                Assert.AreEqual(request.Groups.Count, 
                    await context.Set<RecordAssignedInspectionGroup>().CountAsync());
                Assert.AreEqual(request.Groups.SelectMany(x => x.Items).Count(), 
                    await context.Set<RecordAssignedInspectionItem>().CountAsync());
                Assert.AreEqual(request.Groups.SelectMany(x => x.Items).SelectMany(x => x.Responses).Count(),
                    await context.Set<RecordAssignedInspectionItemResponse>().CountAsync());
            }
        }
    }
}
