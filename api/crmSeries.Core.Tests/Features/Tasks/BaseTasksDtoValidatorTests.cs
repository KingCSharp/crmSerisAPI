using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks;
using crmSeries.Core.Features.Tasks.Dtos;
using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Features.Tasks.Validator;
using NUnit.Framework;
using System;

namespace crmSeries.Core.Tests.Features.Tasks
{
    [TestFixture]
    public class BaseTaskDtoValidatorTests
    {
        private BaseTaskDto _baseTaskDto;
        private BaseTaskDtoValidator _baseTaskDtoValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _baseTaskDtoValidator = new BaseTaskDtoValidator();
        }

        [SetUp]
        public void Setup()
        {
            _baseTaskDto = new BaseTaskDto()
            {
                Subject = "Task Subject"
            };
        }

        [Test]
        public void Validate_MinimumRequiredFieldsPresent_IsValid()
        {
            // Act
            var result = _baseTaskDtoValidator.Validate(_baseTaskDto);

            //Assert 
            Assert.AreEqual(true, result.IsValid);
            Assert.AreEqual(result.Errors.Count, 0);
        }

        [TestCase(TasksConstants.Priorities.Normal, true)]
        [TestCase(TasksConstants.Priorities.High, true)]
        [TestCase(TasksConstants.Priorities.Medium, true)]
        [TestCase("Foobar", false)]

        public void Validate_Priorities_ReturnsAppropriateValidation(string priority,
            bool isValid)
        {
            //Arrange
            _baseTaskDto.Priority = priority;

            // Act
            var result = _baseTaskDtoValidator.Validate(_baseTaskDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.AreEqual(TasksConstants.ErrorMessages.InvalidPriority, result.Errors[0].ErrorMessage);
            }
        }

        [TestCase(TasksConstants.Statuses.Completed, true)]
        [TestCase(TasksConstants.Statuses.InProcess, true)]
        [TestCase(TasksConstants.Statuses.NotStarted, true)]
        [TestCase("Foobar", false)]

        public void Validate_Statuses_ReturnsAppropriateValidation(string status,
            bool isValid)
        {
            //Arrange
            _baseTaskDto.Status = status;

            // Act
            var result = _baseTaskDtoValidator.Validate(_baseTaskDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.AreEqual(TasksConstants.ErrorMessages.InvalidStatus, result.Errors[0].ErrorMessage);
            }
        }

        [TestCase("Test Subject", true)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void Validate_Subject_ReturnsAppropriateValidation(string subject,
            bool isValid)
        {
            //Arrange
            _baseTaskDto.Subject = subject;

            // Act
            var result = _baseTaskDtoValidator.Validate(_baseTaskDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.AreEqual("'Subject' must not be empty.", result.Errors[0].ErrorMessage);
            }
        }

        [TestCase("1/1/2019", true)]
        public void Validate_DueDate_ReturnsAppropriateValidation(DateTime dueDate,
            bool isValid)
        {
            //Arrange
            _baseTaskDto.DueDate = dueDate;

            // Act
            var result = _baseTaskDtoValidator.Validate(_baseTaskDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.AreEqual(Common.Constants.ErrorMessages.InvalidDate, result.Errors[0].ErrorMessage);
            }
        }

        [TestCase(TasksConstants.MaxSubjectLength, true)]
        [TestCase(TasksConstants.MaxSubjectLength + 1, false)]
        public void Validate_SubjectLength_ReturnsAppropriateValidation(
            int length,
            bool isValid)
        {
            //Arrange
            _baseTaskDto.Subject = TestUtility.GenerateStringOfLength(length);

            // Act
            var result = _baseTaskDtoValidator.Validate(_baseTaskDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains(
                    TestUtility.MaxLengthError(
                        "Subject", TasksConstants.MaxSubjectLength)));
            }
        }

        [TestCase(TasksConstants.MaxStatusLength, 1)]
        [TestCase(TasksConstants.MaxStatusLength + 1, 2)]
        public void Validate_StatusLength_ReturnsAppropriateValidation(
            int length,
            int errorCount)
        {
            //Arrange
            _baseTaskDto.Status = TestUtility.GenerateStringOfLength(length);

            // Act
            var result = _baseTaskDtoValidator.Validate(_baseTaskDto);

            //Assert 
            Assert.AreEqual(errorCount, result.Errors.Count);
            if (!result.IsValid && result.Errors.Count > 1)
            {
                Assert.IsTrue(result.Errors[1].ErrorMessage.Contains(
                    TestUtility.MaxLengthError(
                        "Status", TasksConstants.MaxStatusLength)));
            }
        }

        [TestCase(TasksConstants.MaxPriorityLength, 1)]
        [TestCase(TasksConstants.MaxPriorityLength + 1, 2)]
        public void Validate_PriorityLength_ReturnsAppropriateValidation(
            int length,
            int errorCount)
        {
            //Arrange
            _baseTaskDto.Priority = TestUtility.GenerateStringOfLength(length);

            // Act
            var result = _baseTaskDtoValidator.Validate(_baseTaskDto);

            //Assert 
            Assert.AreEqual(errorCount, result.Errors.Count);
            if (!result.IsValid && result.Errors.Count > 1)
            {
                Assert.IsTrue(result.Errors[1].ErrorMessage.Contains(
                    TestUtility.MaxLengthError(
                        "Priority", TasksConstants.MaxPriorityLength)));
            }
        }

        [TestCase(TasksConstants.MaxReminderRepeatScheduleLength, true)]
        [TestCase(TasksConstants.MaxReminderRepeatScheduleLength + 1, false)]
        public void Validate_ReminderRepeatScheduleLength_ReturnsAppropriateValidation(
            int length,
            bool isValid)
        {
            //Arrange
            _baseTaskDto.ReminderRepeatSchedule = TestUtility.GenerateStringOfLength(length);

            // Act
            var result = _baseTaskDtoValidator.Validate(_baseTaskDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains(
                    TestUtility.MaxLengthError(
                        "Reminder Repeat Schedule", TasksConstants.MaxReminderRepeatScheduleLength)));
            }
        }

        [TestCase(TasksConstants.MaxEventIdLength, true)]
        [TestCase(TasksConstants.MaxEventIdLength + 1, false)]
        public void Validate_EventIdLength_ReturnsAppropriateValidation(
            int length,
            bool isValid)
        {
            //Arrange
            _baseTaskDto.EventId = TestUtility.GenerateStringOfLength(length);

            // Act
            var result = _baseTaskDtoValidator.Validate(_baseTaskDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains(
                    TestUtility.MaxLengthError(
                        "Event Id", TasksConstants.MaxEventIdLength)));
            }
        }

        [TestCase(TasksConstants.MaxCalendarIdLength, true)]
        [TestCase(TasksConstants.MaxCalendarIdLength + 1, false)]
        public void Validate_CalendarIdLength_ReturnsAppropriateValidation(
            int length,
            bool isValid)
        {
            //Arrange
            _baseTaskDto.CalendarId = TestUtility.GenerateStringOfLength(length);

            // Act
            var result = _baseTaskDtoValidator.Validate(_baseTaskDto);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.IsTrue(result.Errors[0].ErrorMessage.Contains(
                    TestUtility.MaxLengthError(
                        "Calendar Id", TasksConstants.MaxCalendarIdLength)));
            }
        }
    }
}