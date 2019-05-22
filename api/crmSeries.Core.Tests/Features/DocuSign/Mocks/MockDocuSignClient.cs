using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Features.DocuSign;

namespace crmSeries.Core.Tests.Features.DocuSign.Mocks
{
    internal class MockDocuSignClient : IDocuSignClient
    {
        public Func<Task<List<DocuSignTemplate>>> GetTemplatesImplementation { get; set; }
            = () => Task.FromResult(new List<DocuSignTemplate>());

        public Func<Task> SendTemplateImplementation { get; set; }
            = () => Task.CompletedTask;

        public Task<List<DocuSignTemplate>> GetTemplates()
            => GetTemplatesImplementation();

        public Task SendTemplate(string templateId, IEnumerable<DocuSignTemplateRecipient> recipients)
            => SendTemplateImplementation();
    }
}
