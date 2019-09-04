using System;
using System.Collections.Generic;
using crmSeries.Core.Features.Inspections;
using crmSeries.Core.Features.Inspections.Dtos;
using crmSeries.Core.Features.Inspections.Validator;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Inspections
{
    [TestFixture]
    public class SaveInspectionRecordValidatorTests
    {
        private SaveInspectionRecordRequest _request;
        private RecordAssignedInspectionDtoValidator _inspectionValidator;
        private RecordAssignedInspectionGroupDtoValidator _groupValidator;
        private RecordAssignedInspectionItemDtoValidator _itemValidator;
        private RecordAssignedInspectionItemResponseDtoValidator _itemResponseValidator;
        private SaveInspectionDtoValidator _saveDtoValidator;
        private SaveInspectionRecordValidator _saveValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _itemResponseValidator = new RecordAssignedInspectionItemResponseDtoValidator(
                new BaseRecordAssignedInspectionItemResponseDtoValidator());
            _itemValidator = new RecordAssignedInspectionItemDtoValidator(
                new BaseRecordAssignedInspectionItemDtoValidator(), _itemResponseValidator);
            _groupValidator = new RecordAssignedInspectionGroupDtoValidator(
                new BaseRecordAssignedInspectionGroupDtoValidator(), _itemValidator);
            _inspectionValidator = new RecordAssignedInspectionDtoValidator(
                new BaseRecordAssignedInspectionDtoValidator(), _groupValidator);
            _saveDtoValidator = new SaveInspectionDtoValidator(_inspectionValidator);
            _saveValidator = new SaveInspectionRecordValidator(_saveDtoValidator);
        }

        [SetUp]
        public void Setup()
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
                InspectionName = "Name",
                InspectionType = "Type",
                RecordId = 1,
                RecordType = "Equipment",
                Groups = groupDtos
            };

            _request = new SaveInspectionRecordRequest(inspectionDto);
        }
        
        [Test]
        public void Validate_ValidRequest_IsValid()
        {
            // Arrange

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(true, result.IsValid);
            Assert.AreEqual(result.Errors.Count, 0);
        }

        [Test]
        public void Validate_MissingSaveDto_ReturnsError()
        {
            // Arrange
            _request = new SaveInspectionRecordRequest(null);

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Validate_InvalidAssignedInspectionId_ReturnsError(int assignedInspectionId)
        {
            // Arrange
            _request.Inspection.AssignedInspectionId = assignedInspectionId;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [Test]
        public void Validate_MissingInspectionDto_ReturnsError()
        {
            // Arrange
            _request.Inspection.Inspection = null;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Validate_InvalidInspectionDtoInspectionId_ReturnsError(int inspectionId)
        {
            // Arrange
            _request.Inspection.Inspection.InspectionId = inspectionId;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_EmptyInspectionDtoRecordType_ReturnsError(string recordType)
        {
            // Arrange
            _request.Inspection.Inspection.RecordType = recordType;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Validate_InvalidInspectionDtoRecordId_ReturnsError(int recordId)
        {
            // Arrange
            _request.Inspection.Inspection.RecordId = recordId;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_EmptyInspectionDtoInspectionName_ReturnsError(string inspectionName)
        {
            // Arrange
            _request.Inspection.Inspection.InspectionName = inspectionName;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_EmptyInspectionDtoInspectionType_ReturnsError(string inspectionType)
        {
            // Arrange
            _request.Inspection.Inspection.InspectionType = inspectionType;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(-0.1)]
        public void Validate_InvalidInspectionDtoInspectionHours_ReturnsError(decimal inspectionHours)
        {
            // Arrange
            _request.Inspection.Inspection.InspectionHours = inspectionHours;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(null)]
        public void Validate_MissingInspectionDtoComments_ReturnsError(string comments)
        {
            // Arrange
            _request.Inspection.Inspection.Comments = comments;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [Test]
        public void Validate_MissingInspectionDtoGroups_ReturnsError()
        {
            // Arrange
            _request.Inspection.Inspection.Groups = null;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_EmptyGroupDtoGroupName_ReturnsError(string groupName)
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].GroupName = groupName;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(-1)]
        public void Validate_InvalidGroupDtoSequence_ReturnsError(int sequence)
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].Sequence = sequence;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(null)]
        public void Validate_MissingGroupDtoComments_ReturnsError(string comments)
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].Comments = comments;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [Test]
        public void Validate_MissingGroupDtoItems_ReturnsError()
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].Items = null;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }
        
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_EmptyItemDtoItem_ReturnsError(string item)
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].Items[0].Item = item;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(-1)]
        public void Validate_InvalidItemDtoSequence_ReturnsError(int sequence)
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].Items[0].Sequence = sequence;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_EmptyItemDtoDataType_ReturnsError(string dataType)
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].Items[0].DataType = dataType;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(null)]
        public void Validate_MissingItemDtoResponse_ReturnsError(string response)
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].Items[0].Response = response;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(null)]
        public void Validate_MissingItemDtoComments_ReturnsError(string comments)
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].Items[0].Comments = comments;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [Test]
        public void Validate_MissingItemDtoResponses_ReturnsError()
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].Items[0].Responses = null;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_EmptyItemResponseDtoResponse_ReturnsError(string response)
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].Items[0].Responses[0].Response = response;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }

        [TestCase(-1)]
        public void Validate_InvalidItemResponseDtoSequence_ReturnsError(int sequence)
        {
            // Arrange
            _request.Inspection.Inspection.Groups[0].Items[0].Responses[0].Sequence = sequence;

            // Act
            var result = _saveValidator.Validate(_request);

            // Assert
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(0, result.Errors.Count);
        }
    }
}
