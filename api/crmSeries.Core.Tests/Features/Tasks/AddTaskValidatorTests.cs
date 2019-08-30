using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Tasks
{
    [TestFixture]
    public class AddTaskValidatorTests
    {
        private AddTaskValidator _addTaskValidator;
        private AddTaskRequest _addTaskRequest;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _addTaskValidator = new AddTaskValidator();
        }

        [SetUp]
        public void Setup()
        {
            _addTaskRequest = new AddTaskRequest
            {
                Subject = "Test Subject"
            };
        }

        [TestCase(1, 0, true)]
        [TestCase(0, 0, true)]
        [TestCase(-1, 1, false)]
        public void Validate_ContactId_ReturnsAppropriate(int contactId,
            int numberOfErrors,
            bool isValid)
        {
            //Arrange
            _addTaskRequest.ContactId = contactId;

            // Act
            var result = _addTaskValidator.Validate(_addTaskRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count);

            if (!result.IsValid)
            {
                Assert.AreEqual("'Contact Id' must be greater than '-1'.", result.Errors[0].ErrorMessage);
            }
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("foo")]
        public void Validate_RelatedRecordIdWhenIdIs1_ReturnsValidationFailureIfTypeIsEmpty(string recordType)
        {
            //Arrange
            _addTaskRequest.RelatedRecordId = 1;
            _addTaskRequest.RelatedRecordType = recordType;

            // Act
            var result = _addTaskValidator.Validate(_addTaskRequest);

            //Assert 
            Assert.AreEqual(false, result.IsValid);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(Constants.ErrorMessages.InvalidRecordType, result.Errors[0].ErrorMessage);
        }

        [TestCase(0, "", 0, true)]
        [TestCase(0, "foo", 1, false)]
        public void Validate_RelatedRecordsWhenIdIs0_ReturnsAppropriate(
            int relatedRecordId,
            string relatedRecordType,
            int numberOfErrors,
            bool isValid)
        {
            //Arrange
            _addTaskRequest.RelatedRecordId = relatedRecordId;
            _addTaskRequest.RelatedRecordType = relatedRecordType;

            // Act
            var result = _addTaskValidator.Validate(_addTaskRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count);

            if (!result.IsValid)
            {
                Assert.AreEqual("'Related Record Type' must be empty.", result.Errors[0].ErrorMessage);
            }
        }

        [TestCase(Constants.RelatedRecord.Types.Company, 0, true)]
        [TestCase(Constants.RelatedRecord.Types.Contact, 0, true)]
        [TestCase(Constants.RelatedRecord.Types.Equipment, 0, true)]
        [TestCase(Constants.RelatedRecord.Types.Lead, 0, true)]
        [TestCase(Constants.RelatedRecord.Types.Note, 0, true)]
        [TestCase(Constants.RelatedRecord.Types.Opportunity, 0, true)]
        [TestCase(Constants.RelatedRecord.Types.Task, 0, true)]
        [TestCase(Constants.RelatedRecord.Types.User, 0, true)]
        [TestCase("invalid type", 1, false)]
        public void Validate_RelatedRecordType_ReturnsAppropriateValidation(
            string relatedRecordType,
            int numberOfErrors,
            bool isValid)
        {
            //Arrange
            _addTaskRequest.RelatedRecordId = 1;
            _addTaskRequest.RelatedRecordType = relatedRecordType;

            // Act
            var result = _addTaskValidator.Validate(_addTaskRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            Assert.AreEqual(numberOfErrors, result.Errors.Count);

            if (!result.IsValid)
            {
                Assert.AreEqual(Constants.ErrorMessages.InvalidRecordType, result.Errors[0].ErrorMessage);
            }
        }
    }
}
