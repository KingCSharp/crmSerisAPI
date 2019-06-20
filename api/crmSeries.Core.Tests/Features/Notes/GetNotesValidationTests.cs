using crmSeries.Core.Common;
using crmSeries.Core.Features.Notes;
using NUnit.Framework;
using System;

namespace crmSeries.Core.Tests.Features.Notes
{
    [TestFixture]
    public class GetNotesValidatorTests
    {
        private GetNotesRequest _getNotesRequest;
        private GetNotesValidator _getNotesValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _getNotesValidator = new GetNotesValidator();
        }

        [SetUp]
        public void Setup()
        {
            _getNotesRequest = new GetNotesRequest
            {
                PageNumber = 1,
                PageSize = 20
            };
        }

        [Test]
        public void Validate_NormalRequest_NoValidataionIssuesFound()
        {
            //Arrange

            // Act
            var result = _getNotesValidator.Validate(_getNotesRequest);

            //Assert 
            Assert.AreEqual(true, result.IsValid);
            Assert.AreEqual(0, result.Errors.Count);
        }

        public void Validate_ToDateAndFromDateAreTheSame_NoValidataionIssuesReturned()
        {
            //Arrange
            _getNotesRequest.FromDate = new DateTime(2019, 5, 5);
            _getNotesRequest.ToDate = new DateTime(2019, 5, 5);

            // Act
            var result = _getNotesValidator.Validate(_getNotesRequest);

            //Assert 
            Assert.AreEqual(true, result.IsValid);
            Assert.AreEqual(0, result.Errors.Count);
        }

        [Test]
        public void Validate_ToDateIsLessThanFromDate_ValidataionIssueIsReturned()
        {
            //Arrange
            _getNotesRequest.FromDate = new DateTime(2019, 5, 6);
            _getNotesRequest.ToDate = new DateTime(2019, 5, 5);

            // Act
            var result = _getNotesValidator.Validate(_getNotesRequest);

            //Assert 
            Assert.AreEqual(false, result.IsValid);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(Constants.ErrorMessages.FromDateLessThanDate, result.Errors[0].ErrorMessage);
        }
    }
}
