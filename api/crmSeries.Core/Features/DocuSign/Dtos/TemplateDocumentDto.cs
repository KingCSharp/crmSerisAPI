using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.DocuSign.Dtos
{
    public class TemplateDocumentDto
    {
        public string DocumentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }
}
