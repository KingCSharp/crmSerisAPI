using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Features.DocuSign;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.DocuSign
{
    [TestFixture]
    public class SendTemplateRequestHandlerTests
    {
        private Mocks.MockDocuSignClient _docuSignClient;
        private SendTemplateRequestHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _docuSignClient = new Mocks.MockDocuSignClient();
            _handler = new SendTemplateRequestHandler(_docuSignClient);
        }

        [Test]
        public async Task Handle_ValidRequest_CallsDocuSignClient()
        {
            // Arrange
            var request = CreateValidRequest();
            var called = false;

            _docuSignClient.SendTemplateImplementation = 
                () => { called = true; return Task.CompletedTask; };

            // Act
            var response = await _handler.HandleAsync(request);

            // Assert
            Assert.IsFalse(response.HasErrors);
            Assert.IsTrue(called);
        }

        [Test]
        public async Task Handle_DocuSignClientFailure_ReturnsResponseError()
        {
            // Arrange
            var request = CreateValidRequest();
            
            _docuSignClient.SendTemplateImplementation =
                () => throw new Exception("Test");

            // Act
            var response = await _handler.HandleAsync(request);

            // Assert
            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual("Failed to send DocuSign template: Test", response.Errors[0].ErrorMessage);
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
