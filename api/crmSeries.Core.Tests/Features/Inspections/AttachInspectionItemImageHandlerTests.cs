using System.IO;
using System.Text;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.FileStorage;
using crmSeries.Core.Features.Inspections;
using crmSeries.Core.Features.Inspections.Utility;
using crmSeries.Core.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;

namespace crmSeries.Core.Tests.Features.Inspections
{
    public class AttachInspectionItemImageHandlerTests : BaseUnitTest
    {
        private AttachInspectionItemImageRequest _request;
        private MockFileStorageProvider _storageProvider;
        private Stream _fileStream;

        [SetUp]
        public void TestSetUp()
        {
            var fileContents = "This is a test file.";
            var bytes = Encoding.ASCII.GetBytes(fileContents);

            _storageProvider = Container.GetInstance<IFileStorageProvider>() as MockFileStorageProvider;
            _storageProvider.GenerateMockPath = true;

            _fileStream = new MemoryStream(bytes);

            _request = new AttachInspectionItemImageRequest
            {
                AssignedInspectionId = 0,
                AssignedItemId = 0,
                FileName = "File.txt",
                FileLength = bytes.Length,
                FileStream = _fileStream
            };
        }

        [TearDown]
        public void TestTearDown()
        {
            _fileStream.Dispose();
        }

        [Test]
        public async Task HandleAsync_WithValidRequest_ShouldCreateRecord()
        {
            // Arrange
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var inspection = new RecordAssignedInspection();
                context.Add(inspection);

                var item = new RecordAssignedInspectionItem();
                context.Add(item);

                await context.SaveChangesAsync();

                _request.AssignedInspectionId = inspection.AssignedInspectionId;
                _request.AssignedItemId = item.AssignedItemId;
                var handler = new AttachInspectionItemImageHandler(context, _storageProvider);

                // Act
                var response = await handler.HandleAsync(_request);

                // Assert
                Assert.AreEqual(false, response.HasErrors);
                Assert.AreEqual(1, await context.Set<RecordAssignedInspectionImage>().CountAsync());
            }
        }

        [TestCase(99999)]
        public async Task HandleAsync_WithInvalidAssignedInspectionId_ShouldReturnError(int assignedInspectionId)
        {
            // Arrange
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var item = new RecordAssignedInspectionItem();
                context.Add(item);

                await context.SaveChangesAsync();

                _request.AssignedInspectionId = assignedInspectionId;
                _request.AssignedItemId = item.AssignedItemId;
                var handler = new AttachInspectionItemImageHandler(context, _storageProvider);

                // Act
                var response = await handler.HandleAsync(_request);

                // Assert
                Assert.AreEqual(true, response.HasErrors);
                Assert.AreEqual(InspectionConstants.ErrorMessages.InspectionRecordNotFound, response.Errors[0].ErrorMessage);
            }
        }

        [TestCase(99999)]
        public async Task HandleAsync_WithInvalidAssignedItemId_ShouldReturnError(int assignedItemId)
        {
            // Arrange
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var inspection = new RecordAssignedInspection();
                context.Add(inspection);

                await context.SaveChangesAsync();

                _request.AssignedInspectionId = inspection.AssignedInspectionId;
                _request.AssignedItemId = assignedItemId;
                var handler = new AttachInspectionItemImageHandler(context, _storageProvider);

                // Act
                var response = await handler.HandleAsync(_request);

                // Assert
                Assert.AreEqual(true, response.HasErrors);
                Assert.AreEqual(InspectionConstants.ErrorMessages.InspectionRecordItemNotFound, response.Errors[0].ErrorMessage);
            }
        }

        [Test]
        public async Task HandleAsync_WithFailedImageUpload_ShouldReturnError()
        {
            // Arrange
            var options = GetHeavyEquipmentContextOptions();
            _storageProvider.GenerateMockPath = false;

            using (var context = new HeavyEquipmentContext(options))
            {
                var inspection = new RecordAssignedInspection();
                context.Add(inspection);

                var item = new RecordAssignedInspectionItem();
                context.Add(item);

                await context.SaveChangesAsync();

                _request.AssignedInspectionId = inspection.AssignedInspectionId;
                _request.AssignedItemId = item.AssignedItemId;
                var handler = new AttachInspectionItemImageHandler(context, _storageProvider);

                // Act
                var response = await handler.HandleAsync(_request);

                // Assert
                Assert.AreEqual(true, response.HasErrors);
                Assert.AreEqual(InspectionConstants.ErrorMessages.ImageUploadFailed, response.Errors[0].ErrorMessage);
            }
        }
    }
}
