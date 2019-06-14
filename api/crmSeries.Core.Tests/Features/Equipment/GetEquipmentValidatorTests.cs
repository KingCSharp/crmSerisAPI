using crmSeries.Core.Features.Equipment;
using crmSeries.Core.Features.Equipment.Utility;
using crmSeries.Core.Logic.Queries;
using NUnit.Framework;
using System.Collections.Generic;

namespace crmSeries.Core.Tests.Features.Equipment
{
    [TestFixture]
    public class GetEquipmentValidatorTests
    {
        private GetEquipmentRequest _getEquipmentRequest;
        private GetEquipmentValidator _getEquipmentValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _getEquipmentValidator = new GetEquipmentValidator();
        }

        [SetUp]
        public void Setup()
        {
            _getEquipmentRequest = new GetEquipmentRequest
            {
                PageInfo = new PagedQueryRequest
                {
                    PageNumber = 1,
                    PageSize = 50
                }
            };
        }

        [TestCase(1, true)]
        [TestCase(35, true)]
        [TestCase(0, false)]
        [TestCase(-1, false)]
        public void Validate_PageNumber_ValidatesCorrectly(
            int pageNumber,
            bool isValid)
        {
            //Arrange
            _getEquipmentRequest.PageInfo.PageNumber = pageNumber;

            // Act
            var result = _getEquipmentValidator.Validate(_getEquipmentRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.AreEqual("'Page Info. Page Number' must be greater than '0'.", result.Errors[0].ErrorMessage);
            }
        }

        [TestCase(1, true)]
        [TestCase(35, true)]
        [TestCase(0, false)]
        [TestCase(-1, false)]
        public void Validate_PageSize_ValidatesCorrectly(
            int pageSize,
            bool isValid)
        {
            //Arrange
            _getEquipmentRequest.PageInfo.PageSize = pageSize;

            // Act
            var result = _getEquipmentValidator.Validate(_getEquipmentRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.AreEqual("'Page Info. Page Size' must be greater than '0'.", result.Errors[0].ErrorMessage);
            }
        }

        [TestCase(EquipmentConstants.StatusMaxLength, true)]
        [TestCase(EquipmentConstants.StatusMaxLength + 1, false)]
        public void Validate_StatusEqualsMaxLength_ReturnsAppropriateValidation(
            int length,
            bool isValid)
        {
            //Arrange
            _getEquipmentRequest.Statuses = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                _getEquipmentRequest.Statuses.Add(TestUtility.GenerateStringOfLength(length));
            }

            // Act
            var result = _getEquipmentValidator.Validate(_getEquipmentRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.AreEqual(EquipmentConstants.ErrorMessages.ExceededStatusMaxLength, result.Errors[0].ErrorMessage);
            }
        }
    }
}
