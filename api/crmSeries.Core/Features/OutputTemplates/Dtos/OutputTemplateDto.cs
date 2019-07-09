using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.OutputTemplates.Dtos
{
    public class BaseOutputTemplateDto
    {
        /// <summary>
        /// The foreign key unnique identifier for the category of this output template.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// The document type for the template. E.g., Word, PDF, etc.
        /// </summary>
        public string TemplateType { get; set; }

        /// <summary>
        /// Provides information about this template.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The full URI path to this template file.
        /// </summary>
        public string AbsoluteUri { get; set; }

        /// <summary>
        /// The filename of the template file. E.g., MyTemplate.docx
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// The MIME type of this template.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// The external source of this template. E.g., DocuSign, RightSignature, etc.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The unique identifier of this template from its source.
        /// </summary>
        public string SourceId { get; set; }
    }

    public class GetOutputTemplateDto : BaseOutputTemplateDto
    {
        /// <summary>
        /// The unique identifier of this output template entity.
        /// </summary>
        public int TemplateId { get; set; }
    }

    public class AddOutputTemplateDto : BaseOutputTemplateDto
    {
    }

    public class EditOutputTemplateDto : BaseOutputTemplateDto
    {
        /// <summary>
        /// The unique identifier of this output template entity.
        /// </summary>
        public int TemplateId { get; set; }
    }
}
