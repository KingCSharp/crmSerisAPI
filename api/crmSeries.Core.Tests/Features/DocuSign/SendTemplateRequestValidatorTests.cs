using System.Collections.Generic;
using crmSeries.Core.Features.DocuSign;
using FluentValidation;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.DocuSign
{
    [TestFixture]
    public class SendTemplateRequestValidatorTests
    {
        private readonly IValidator<TemplateDto> _templateValidator
            = new TemplateDtoValidator();

        private readonly IValidator<TemplateRecipientDto> _recipientValidator
            = new TemplateRecipientDtoValidator();

        [Test]
        public void Validate_NullTemplate_ReturnsValidationError()
        {
            // Arrange
            var request = CreateValidRequest();
            request.Template = null;

            // Act
            var validator = new SendTemplateRequestValidator(_templateValidator, _recipientValidator);
            var result = validator.Validate(request);

            // Assert 
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("'Template' must not be empty.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void Validate_NullRecipients_ReturnsValidationError()
        {
            // Arrange
            var request = CreateValidRequest();
            request.Recipients = null;

            // Act
            var validator = new SendTemplateRequestValidator(_templateValidator, _recipientValidator);
            var result = validator.Validate(request);

            // Assert 
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("'Recipients' must not be empty.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void Validate_EmptyRecipients_ReturnsValidationError()
        {
            // Arrange
            var request = CreateValidRequest();
            request.Recipients = new List<TemplateRecipientDto>();

            // Act
            var validator = new SendTemplateRequestValidator(_templateValidator, _recipientValidator);
            var result = validator.Validate(request);

            // Assert 
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("'Recipients' must not be empty.", result.Errors[0].ErrorMessage);
        }

        private static SendTemplateRequest CreateValidRequest() =>
            new SendTemplateRequest
            {
                Template = new TemplateDto { TemplateId = "ValidId" },
                Recipients = new List<TemplateRecipientDto>
                {
                    new TemplateRecipientDto { Name = "ValidName", Role = "ValidRole", Email = "Valid@Email" }
                }
            };
    }
}
