using crmSeries.Core.Common;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Features.Notes.Utility;
using crmSeries.Core.Features.Notes.Validator;
using NUnit.Framework;
using System;

namespace crmSeries.Core.Tests.Features.Notes
{
    [TestFixture]
    public class BaseNoteDtoValidatorTests
    {
        private BaseNoteDto _baseNoteDto;
        private BaseNoteDtoValidator _baseNoteDtoValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _baseNoteDtoValidator = new BaseNoteDtoValidator();
        }

        [SetUp]
        public void Setup()
        {
            _baseNoteDto = new BaseNoteDto()
            {
                RecordId = 1,
                UserId = 1,
                RecordTypeId = 1,
                Comments = "Test Comments",
                NoteDate = DateTime.Now
            };
        }

        [TestCase(1, 0, true)]
        [TestCase(-1, 1, false)]
        [TestCase(0, 1, false)]
        public void Validate_RecordId_ReturnsAppropriate(int recordId,
            int numberOfErrors,
            bool isValid)
        {
            //Arrange
            _baseNoteDto.RecordId = recordId;

            // Act
            var result = _baseNoteDtoValidator.Validate(_baseNoteDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count);

            if (!result.IsValid)
            {
                Assert.AreEqual("'Record Id' must be greater than '0'.", result.Errors[0].ErrorMessage);
            }
        }

        [TestCase(1, 0, true)]
        [TestCase(-1, 1, false)]
        [TestCase(0, 1, false)]
        public void Validate_UserId_ReturnsAppropriate(int userId,
            int numberOfErrors,
            bool isValid)
        {
            //Arrange
            _baseNoteDto.UserId = userId;

            // Act
            var result = _baseNoteDtoValidator.Validate(_baseNoteDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count);

            if (!result.IsValid)
            {
                Assert.AreEqual("'User Id' must be greater than '0'.", result.Errors[0].ErrorMessage);
            }
        }

        [TestCase(1, 0, true)]
        [TestCase(-1, 1, false)]
        [TestCase(0, 1, false)]
        public void Validate_RecordTypeId_ReturnsAppropriate(int recordTypeId,
            int numberOfErrors,
            bool isValid)
        {
            //Arrange
            _baseNoteDto.RecordTypeId = recordTypeId;

            // Act
            var result = _baseNoteDtoValidator.Validate(_baseNoteDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count);

            if (!result.IsValid)
            {
                Assert.AreEqual("'Record Type Id' must be greater than '0'.", result.Errors[0].ErrorMessage);
            }
        }

        [TestCase("Test Comments", true)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void Validate_Comments_ReturnsAppropriateValidation(string comments,
            bool isValid)
        {
            //Arrange
            _baseNoteDto.Comments = comments;

            // Act
            var result = _baseNoteDtoValidator.Validate(_baseNoteDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.AreEqual("'Comments' must not be empty.", result.Errors[0].ErrorMessage);
            }
        }

        [TestCase(55.5, true)]
        [TestCase(-91, false)]
        [TestCase(91, false)]
        public void Validate_Latitude_ReturnsAppropriateValidation(decimal latitude,
            bool isValid)
        {
            //Arrange
            _baseNoteDto.Latitude = latitude;

            // Act
            var result = _baseNoteDtoValidator.Validate(_baseNoteDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.AreEqual(NotesConstants.ErrorMessages.InvalidLatitude, result.Errors[0].ErrorMessage);
            }
        }

        [TestCase(55.5, true)]
        [TestCase(-181, false)]
        [TestCase(181, false)]
        public void Validate_Longitude_ReturnsAppropriateValidation(decimal longitude,
            bool isValid)
        {
            //Arrange
            _baseNoteDto.Longitude = longitude;

            // Act
            var result = _baseNoteDtoValidator.Validate(_baseNoteDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.AreEqual(NotesConstants.ErrorMessages.InvalidLongitude, result.Errors[0].ErrorMessage);
            }
        }

        [TestCase("1/1/2019", true)]
        public void Validate_DueDate_ReturnsAppropriateValidation(DateTime noteDate,
            bool isValid)
        {
            //Arrange
            _baseNoteDto.NoteDate = noteDate;

            // Act
            var result = _baseNoteDtoValidator.Validate(_baseNoteDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.AreEqual(Constants.ErrorMessages.InvalidDate, result.Errors[0].ErrorMessage);
            }
        }
    }
}
