using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.DocuSign.Dtos
{
    public class TemplateFieldDto
    {
        public string TabId { get; set; }
        public string DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string TabLabel { get; set; }
        public bool Required { get; set; }
        public bool Locked { get; set; }
        public string Type { get; set; }
    }
}
