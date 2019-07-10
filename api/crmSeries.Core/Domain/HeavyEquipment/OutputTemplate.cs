using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class OutputTemplate
    {
        public int TemplateId { get; set; }
        public int CategoryId { get; set; }
        public string Template { get; set; }
        public string TemplateType { get; set; }
        public string Description { get; set; }
        public string AbsoluteUri { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Source { get; set; }
        public string SourceId { get; set; }
    }
}
