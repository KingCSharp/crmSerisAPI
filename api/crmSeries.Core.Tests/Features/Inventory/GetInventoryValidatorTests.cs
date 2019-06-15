using crmSeries.Core.Features.Inventory;
using crmSeries.Core.Features.Inventory.Utility;
using crmSeries.Core.Logic.Queries;
using NUnit.Framework;
using System.Collections.Generic;

namespace crmSeries.Core.Tests.Features.Inventory
{
    [TestFixture]
    public class GetInventoryValidatorTests
    {
        private GetInventoryRequest _getInventoryRequest;
        private GetInventoryValidator _getInventoryValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _getInventoryValidator = new GetInventoryValidator();
        }

        [SetUp]
        public void Setup()
        {
            _getInventoryRequest = new GetInventoryRequest
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
            _getInventoryRequest.PageInfo.PageNumber = pageNumber;

            // Act
            var result = _getInventoryValidator.Validate(_getInventoryRequest);

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
            _getInventoryRequest.PageInfo.PageSize = pageSize;

            // Act
            var result = _getInventoryValidator.Validate(_getInventoryRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
            {
                Assert.AreEqual("'Page Info. Page Size' must be greater than '0'.", result.Errors[0].ErrorMessage);
            }
        }

        [TestCase(InventoryConstants.StatusMaxLength, true)]
        [TestCase(InventoryConstants.StatusMaxLength + 1, false)]
        public void Validate_StatusEqualsMaxLength_ReturnsAppropriateValidation(
            int length,
            bool isValid)
        {
            //Arrange
            _getInventoryRequest.Statuses = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                _getInventoryRequest.Statuses.Add(TestUtility.GenerateStringOfLength(length));
            }

            // Act
            var result = _getInventoryValidator.Validate(_getInventoryRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);
            if (!result.IsValid)
            {
                Assert.AreEqual(InventoryConstants.ErrorMessages.ExceededStatusMaxLength, result.Errors[0].ErrorMessage);
            }
        }
    }
}
