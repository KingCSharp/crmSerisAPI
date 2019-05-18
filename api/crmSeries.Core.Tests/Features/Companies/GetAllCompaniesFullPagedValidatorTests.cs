using crmSeries.Core.Features.Companies;
using crmSeries.Core.Logic.Queries;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Companies
{
    [TestFixture]
    public class GetAllCompaniesFullPagedValidatorTests
    {
        private GetAllCompaniesFullPagedRequest _getAllCompaniesFullPagedRequest;
        private GetAllCompaniesFullPagedValidator _getAllCompaniesFullPagedValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _getAllCompaniesFullPagedValidator = new GetAllCompaniesFullPagedValidator();
        }

        [SetUp]
        public void Setup()
        {
            _getAllCompaniesFullPagedRequest = new GetAllCompaniesFullPagedRequest
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
            _getAllCompaniesFullPagedRequest.Query = new PagedQueryRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            // Act
            var result = _getAllCompaniesFullPagedValidator.Validate(_getAllCompaniesFullPagedRequest);

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
            _getAllCompaniesFullPagedRequest.Query = new PagedQueryRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            // Act
            var result = _getAllCompaniesFullPagedValidator.Validate(_getAllCompaniesFullPagedRequest);

            //Assert 
            Assert.AreEqual(result.IsValid, isValid);
            Assert.AreEqual(result.Errors.Count, errorCount);
        }
    }
}
