using crmSeries.Core.Features.Companies;
using NUnit.Framework;

namespace crmSeries.Tests.Core.Features.Companies
{
    [TestFixture]
    public class CompanyValidatorTests
    {
        private GetCompanyValidator _getCompanyValidator;
        private GetCompanyRequest _getCompanyRequest;

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

        [TestCase(null, false)]
        [TestCase(1, true)]
        public void Validate_CompanyIdEmpty_ReturnsNotValid(
            int companyId,
            bool isValid)
        {
            // Arrange 
            _getCompanyRequest.CompanyId = companyId;

            // Act
            var result = _getCompanyValidator.Validate(_getCompanyRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
                Assert.IsNotEmpty(result.Errors[0].ErrorMessage);
        }

        [TestCase(0, false)]
        [TestCase(-57, false)]
        [TestCase(1, true)]
        [TestCase(987, true)]
        public void Validate_CompanyIdNotGreaterThanZero_ReturnsNotValid(
            int companyId,
            bool isValid)
        {
            // Arrange 
            _getCompanyRequest.CompanyId = companyId;

            // Act
            var result = _getCompanyValidator.Validate(_getCompanyRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
                Assert.IsNotEmpty(result.Errors[0].ErrorMessage);
        }
    }
}
