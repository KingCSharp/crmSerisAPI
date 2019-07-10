using crmSeries.Core.Features.DocuSign;
using crmSeries.Core.Features.DocuSign.Dtos;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.DocuSign
{
    [TestFixture]
    public class TemplateRecipientDtoValidatorTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_EmptyRole_ReturnsValidationError(string role)
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Role = role;

            // Act
            var result = new TemplateRecipientDtoValidator().Validate(dto);

            // Assert 
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("'Role' must not be empty.", result.Errors[0].ErrorMessage);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_EmptyName_ReturnsValidationError(string name)
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Name = name;

            // Act
            var result = new TemplateRecipientDtoValidator().Validate(dto);

            // Assert 
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("'Name' must not be empty.", result.Errors[0].ErrorMessage);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Validate_EmptyEmail_ReturnsValidationError(string email)
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Email = email;

            // Act
            var result = new TemplateRecipientDtoValidator().Validate(dto);

            // Assert 
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("'Email' must not be empty.", result.Errors[0].ErrorMessage);
        }

        [TestCase("InvalidEmail")]
        [TestCase("Invalid@Email@")]
        public void Validate_InvalidEmailFormat_ReturnsValidationError(string email)
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Email = email;

            // Act
            var result = new TemplateRecipientDtoValidator().Validate(dto);

            // Assert 
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("An invalid recipient email address was provided.", result.Errors[0].ErrorMessage);
        }

        private static TemplateRecipientDto CreateValidDto() =>
            new TemplateRecipientDto
            {
                Name = "ValidName",
                Email = "Valid@Email",
                Role = "ValidRole"
            };
    }
}
