using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Features.DocuSign;
using crmSeries.Core.Features.DocuSign.Utility;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.DocuSign
{
    [TestFixture]
    public class ListTemplatesRequestHandlerTests : BaseUnitTest
    {
        private Mocks.MockDocuSignClient _docuSignClient;
        private ListTemplatesRequestHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _docuSignClient = new Mocks.MockDocuSignClient();
            _handler = new ListTemplatesRequestHandler(_docuSignClient);
        }

        [Test]
        public async Task Handle_ValidRequest_RetrievesTemplatesFromDocuSignClient()
        {
            // Arrange
            var request = new ListTemplatesRequest();

            _docuSignClient.GetTemplatesImplementation =
                () => Task.FromResult(new List<DocuSignTemplate>
                {
                    new DocuSignTemplate { Name = "TestTemplate" }
                });

            // Act
            var response = await _handler.HandleAsync(request);

            // Assert
            Assert.IsFalse(response.HasErrors);
            Assert.NotNull(response.Data);
            Assert.AreEqual(1, response.Data.Count);
            Assert.AreEqual("TestTemplate", response.Data[0].Name);
        }

        [Test]
        public async Task Handle_DocuSignClientFailure_ReturnsResponseError()
        {
            // Arrange
            var request = new ListTemplatesRequest();
            
            _docuSignClient.GetTemplatesImplementation =
                () => throw new Exception("Test");

            // Act
            var response = await _handler.HandleAsync(request);

            // Assert
            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual("Failed to retrieve DocuSign templates: Test", response.Errors[0].ErrorMessage);
        }
    }
}
