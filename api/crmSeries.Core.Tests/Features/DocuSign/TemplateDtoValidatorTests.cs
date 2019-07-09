using crmSeries.Core.Features.DocuSign;
using crmSeries.Core.Features.DocuSign.Dtos;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.DocuSign
{
    [TestFixture]
    public class TemplateDtoValidatorTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_EmptyTemplateId_ReturnsValidationError(string templateId)
        {
            // Arrange
            var dto = new TemplateDto { TemplateId = templateId };

            // Act
            var result = new TemplateDtoValidator().Validate(dto);

            // Assert 
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("'Template Id' must not be empty.", result.Errors[0].ErrorMessage);
        }
    }
}
