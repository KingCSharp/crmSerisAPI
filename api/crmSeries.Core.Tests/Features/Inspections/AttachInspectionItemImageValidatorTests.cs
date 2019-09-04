using System.IO;
using System.Text;
using crmSeries.Core.Features.Inspections;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Inspections
{
    [TestFixture]
    public class AttachInspectionItemImageValidatorTests
    {
        private AttachInspectionItemImageRequest _request;
        private AttachInspectionItemImageValidator _validator;
        private Stream _fileStream;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _validator = new AttachInspectionItemImageValidator();
        }

        [SetUp]
        public void Setup()
        {
            var fileContents = "This is a test file.";
            var bytes = Encoding.ASCII.GetBytes(fileContents);

            _fileStream = new MemoryStream(bytes);

            _request = new AttachInspectionItemImageRequest
            {
                AssignedInspectionId = 1,
                AssignedItemId = 1,
                FileName = "File.txt",
                FileLength = bytes.Length,
                FileStream = _fileStream
            };
        }

        [TearDown]
        public void TearDown()
        {
            _fileStream.Dispose();
        }
        
        [Test]
        public void Validate_ValidRequest_IsValid()
        {
            // Arrange 
            
            // Act
            var result = _validator.Validate(_request);

            // Assert 
            Assert.AreEqual(true, result.IsValid);
            Assert.AreEqual(result.Errors.Count, 0);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Validate_InvalidAssignedInspectionId_ReturnsError(int assignedInspectionId)
        {
            // Arrange
            _request.AssignedInspectionId = assignedInspectionId;

            // Act
            var result = _validator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Validate_InvalidAssignedItemId_ReturnsError(int assignedItemId)
        {
            // Arrange
            _request.AssignedItemId = assignedItemId;

            // Act
            var result = _validator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Validate_InvalidFileLength_ReturnsError(int fileLength)
        {
            // Arrange
            _request.FileLength = fileLength;

            // Act
            var result = _validator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_InvalidFileName_ReturnsError(string fileName)
        {
            // Arrange
            _request.FileName = fileName;

            // Act
            var result = _validator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [Test]
        public void Validate_MissingFileStream_ReturnsError()
        {
            // Arrange
            _request.FileStream = null;

            // Act
            var result = _validator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }
    }
}
