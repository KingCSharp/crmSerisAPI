using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Features.DocuSign;
using crmSeries.Core.Features.DocuSign.Dtos;
using crmSeries.Core.Features.DocuSign.Utility;

namespace crmSeries.Core.Tests.Features.DocuSign.Mocks
{
    internal class MockDocuSignClient : IDocuSignClient
    {
        public Func<Task<List<DocuSignTemplate>>> GetTemplatesImplementation { get; set; }
            = () => Task.FromResult(new List<DocuSignTemplate>());

        public Func<Task<GetTemplateFullDto>> GetTemplateByIdImplementation { get; set; }
            = () => Task.FromResult(new GetTemplateFullDto());

        public Func<Task<List<TemplateFieldDto>>> GetTemplateFieldsImplementation { get; set; }
            = () => Task.FromResult(new List<TemplateFieldDto>());

        public Func<Task> SendTemplateImplementation { get; set; }
            = () => Task.CompletedTask;

        public Task<List<DocuSignTemplate>> GetTemplates()
            => GetTemplatesImplementation();

        public Task<GetTemplateFullDto> GetTemplateById(string templateId)
            => GetTemplateByIdImplementation();

        public Task<List<TemplateFieldDto>> GetTemplateFields(string templateId)
            => GetTemplateFieldsImplementation();

        public Task SendTemplate(string templateId, IEnumerable<DocuSignTemplateRecipient> recipients, int recordId)
            => SendTemplateImplementation();
    }
}
