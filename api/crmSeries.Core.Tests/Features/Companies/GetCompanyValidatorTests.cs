using crmSeries.Core.Features.Companies;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Companies
{
    [TestFixture]
    public class GetCompanyValidatorTests
    {
        private GetCompanyRequest _getCompanyRequest;
        private GetCompanyValidator _getCompanyValidator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _getCompanyValidator = new GetCompanyValidator();
        }

        [SetUp]
        public void Setup()
        {
            _getCompanyRequest = new GetCompanyRequest
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
            _getCompanyRequest.CompanyId = companyId;

            // Act
            var result = _getCompanyValidator.Validate(_getCompanyRequest);

            //Assert 
            Assert.AreEqual(true, result.IsValid);
            Assert.AreEqual(result.Errors.Count, 0);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(null)]
        public void Validate_InvalidCompanyId_ReturnsAppropriateMessage(int companyId)
        {
            // Arrange 
            _getCompanyRequest.CompanyId = companyId;

            // Act
            var result = _getCompanyValidator.Validate(_getCompanyRequest);

            //Assert 
            Assert.AreEqual(false, result.IsValid);
            Assert.AreNotEqual(result.Errors.Count, 0);
        }
    }
}
