using crmSeries.Core.Features.Companies;
using crmSeries.Core.Logic.Queries;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Companies
{
    [TestFixture]
    public class GetCompaniesFullPagedValidatorTests
    {
        private GetCompaniesFullPagedRequest _getCompaniesFullPagedRequest;
        private GetCompaniesFullPagedValidator _getCompaniesFullPagedValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _getCompaniesFullPagedValidator = new GetCompaniesFullPagedValidator();
        }

        [SetUp]
        public void Setup()
        {
            _getCompaniesFullPagedRequest = new GetCompaniesFullPagedRequest
            {
                Query = new PagedQueryRequest
                {
                    PageNumber = 1,
                    PageSize = 50
                }
            };
        }

        [TestCase(1, 1)]
        [TestCase(35, 35)]
        [TestCase(5783, 5783)]
        public void Validate_ValidPageNumberAndPageSize_IsValid(
            int pageNumber,
            int pageSize)
        {
            // Arrange 
            _getCompaniesFullPagedRequest.Query = new PagedQueryRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            // Act
            var result = _getCompaniesFullPagedValidator.Validate(_getCompaniesFullPagedRequest);

            //Assert 
            Assert.AreEqual(true, result.IsValid);
            Assert.AreEqual(result.Errors.Count, 0);
        }

        [TestCase(1, 1, 0, true)]
        [TestCase(1, 0, 1, false)]
        [TestCase(0, 1, 1, false)]
        [TestCase(0, 0, 2, false)]
        [TestCase(1, -1, 1, false)]
        [TestCase(-1, 1, 1, false)]
        [TestCase(-1, -1, 2, false)]
        public void Validate_InvalidPageNumberAndPageSize_InvalidWithErrors(
            int pageNumber,
            int pageSize,
            int errorCount,
            bool isValid)
        {
            // Arrange 
            _getCompaniesFullPagedRequest.Query = new PagedQueryRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            // Act
            var result = _getCompaniesFullPagedValidator.Validate(_getCompaniesFullPagedRequest);

            //Assert 
            Assert.AreEqual(result.IsValid, isValid);
            Assert.AreEqual(result.Errors.Count, errorCount);
        }
    }
}
