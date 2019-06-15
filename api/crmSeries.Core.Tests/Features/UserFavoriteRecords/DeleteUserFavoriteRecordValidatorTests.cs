using crmSeries.Core.Features.UserFavoriteRecords;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.UserFavoriteRecords
{
    [TestFixture]
    public class DeleteUserFavoriteRecordValidatorTests
    {
        private DeleteUserFavoriteRecordRequest _deleteUserFavoriteRecordRequest;
        private DeleteUserFavoriteRecordValidator _deleteUserFavoriteRecordValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _deleteUserFavoriteRecordValidator = new DeleteUserFavoriteRecordValidator();
        }

        [TestCase(1, true)]
        [TestCase(0, false, "'User Favorite Record Id' must be greater than '0'.")]
        [TestCase(-1, false, "'User Favorite Record Id' must be greater than '0'.")]
        public void Validate_UserFavoriteRecordId_Validates(
            int userFavoriteRecordId,
            bool isValid,
            string errorMessage = null)
        {
            // Arrange 
            _deleteUserFavoriteRecordRequest = new DeleteUserFavoriteRecordRequest(userFavoriteRecordId);

            // Act
            var result = _deleteUserFavoriteRecordValidator.Validate(_deleteUserFavoriteRecordRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if(!isValid)
                Assert.AreEqual(errorMessage, result.Errors[0].ErrorMessage);
        }
    }
}
