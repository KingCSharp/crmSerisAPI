using crmSeries.Core.Features.Companies;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Companies
{
    [TestFixture]
    public class GetCompanyFullValidatorTests
    {
        private GetCompanyFullRequest _getCompanyFullRequest;
        private GetCompanyFullValidator _getCompanyFullValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _getCompanyFullValidator = new GetCompanyFullValidator();
        }

        [SetUp]
        public void Setup()
        {
            _getCompanyFullRequest = new GetCompanyFullRequest
            {
                CompanyId = 1
            };
        }

        [TestCase(1)]
        [TestCase(35)]
        [TestCase(5783)]
        public void Validate_ValidCompanyId_IsValid(int companyId)
        {
            // Arrange 
            _getCompanyFullRequest.CompanyId = companyId;

            // Act
            var result = _getCompanyFullValidator.Validate(_getCompanyFullRequest);

            //Assert 
            Assert.AreEqual(true, result.IsValid);
            Assert.AreEqual(result.Errors.Count, 0);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(null)]
        public void Validate_InvalidCompanyId_ResultInvalid(int companyId)
        {
            // Arrange 
            _getCompanyFullRequest.CompanyId = companyId;

            // Act
            var result = _getCompanyFullValidator.Validate(_getCompanyFullRequest);

            //Assert 
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(result.Errors.Count, 0);
        }
    }
}
