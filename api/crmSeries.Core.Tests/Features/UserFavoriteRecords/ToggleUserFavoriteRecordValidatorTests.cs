using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.UserFavoriteRecords;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.UserFavoriteRecords
{
    [TestFixture]
    public class ToggleUserFavoriteRecordValidatorTests
    {
        private ToggleUserFavoriteRecordRequest _toggleUserFavoriteRecordRequest;
        private ToggleUserFavoriteRecordValidator _toggleUserFavoriteRecordValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _toggleUserFavoriteRecordValidator = new ToggleUserFavoriteRecordValidator();
        }

        [SetUp]
        public void Setup()
        {
            _toggleUserFavoriteRecordRequest = new ToggleUserFavoriteRecordRequest
            {
                RecordId = 1,
                RecordType = Constants.RelatedRecord.Types.Company,
                UserId = 1
            };
        }

        [TestCase(1, true)]
        [TestCase(0, true)]
        [TestCase(-1, false, "'User Id' must be greater than '-1'.")]
        public void Validate_UserId_Validates(
            int userId,
            bool isValid,
            string errorMessage = null)
        {
            // Arrange 
            _toggleUserFavoriteRecordRequest.UserId = userId;

            // Act
            var result = _toggleUserFavoriteRecordValidator.Validate(_toggleUserFavoriteRecordRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!isValid)
                Assert.AreEqual(errorMessage, result.Errors[0].ErrorMessage);
        }

        [TestCase(1, true)]
        [TestCase(0, false, "'Record Id' must be greater than '0'.")]
        [TestCase(-1, false, "'Record Id' must be greater than '0'.")]
        public void Validate_RecordId_Validates(
            int recordId,
            bool isValid,
            string errorMessage = null)
        {
            // Arrange 
            _toggleUserFavoriteRecordRequest.RecordId = recordId;

            // Act
            var result = _toggleUserFavoriteRecordValidator.Validate(_toggleUserFavoriteRecordRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!isValid)
                Assert.AreEqual(errorMessage, result.Errors[0].ErrorMessage);
        }

        [TestCase(Constants.RelatedRecord.Types.Contact, true)]
        [TestCase("Foo Bar", false)]
        public void Validate_RecordType_Validates(
            string recordType,
            bool isValid)
        {
            // Arrange 
            _toggleUserFavoriteRecordRequest.RecordType = recordType;

            // Act
            var result = _toggleUserFavoriteRecordValidator.Validate(_toggleUserFavoriteRecordRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!isValid)
                Assert.AreEqual(Constants.ErrorMessages.InvalidRecordType, result.Errors[0].ErrorMessage);
        }
    }
}
