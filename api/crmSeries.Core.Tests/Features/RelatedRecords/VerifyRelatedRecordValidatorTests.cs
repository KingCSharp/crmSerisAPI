using crmSeries.Core.Common;
using crmSeries.Core.Features.RelatedRecords;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static crmSeries.Core.Features.RelatedRecords.VerifyRelatedRecordHandler;

namespace crmSeries.Core.Tests.Features.RelatedRecords
{
    [TestFixture]
    public class VerifyRelatedRecordValidatorTests
    {
        private VerifyRelatedRecordRequest _verifyRelatedRecordRequest;
        private VerifyRelatedRecordValidator _verifyRelatedRecordValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _verifyRelatedRecordValidator = new VerifyRelatedRecordValidator();
        }

        [SetUp]
        public void Setup()
        {
            _verifyRelatedRecordRequest = new VerifyRelatedRecordRequest()
            {
                RecordTypeId = 1,
                RecordType = Constants.RelatedRecord.Types.Company
            };
        }

        [TestCase(Constants.RelatedRecord.Types.Company, true)]
        [TestCase(Constants.RelatedRecord.Types.Contact, true)]
        [TestCase(Constants.RelatedRecord.Types.Equipment, true)]
        [TestCase(Constants.RelatedRecord.Types.Lead, true)]
        [TestCase(Constants.RelatedRecord.Types.Note, true)]
        [TestCase(Constants.RelatedRecord.Types.Opportunity, true)]
        [TestCase(Constants.RelatedRecord.Types.Task, true)]
        [TestCase(Constants.RelatedRecord.Types.User, true)]
        [TestCase("Foo Bar", false, "The specified condition was not met for 'Record Type'.")]
        public void Validate_RecordTypeId_ReturnsAppropriateValidation(string recordType,
            bool isValid,
            string errorMessage = null)
        {
            //Arrange
            _verifyRelatedRecordRequest.RecordType = recordType;

            // Act
            var result = _verifyRelatedRecordValidator.Validate(_verifyRelatedRecordRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.AreEqual(result.Errors[0].ErrorMessage, errorMessage);
            }
        }
    }
}
