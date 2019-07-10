using crmSeries.Core.Features.DocuSign.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.DocuSign.Utility
{
    public interface IDocuSignClient
    {
        Task<List<DocuSignTemplate>> GetTemplates();

        Task<GetTemplateFullDto> GetTemplateById(string templateId);

        Task<List<TemplateFieldDto>> GetTemplateFields(string templateId);

        Task SendTemplate(string templateId, IEnumerable<DocuSignTemplateRecipient> recipients, int recordId);
    }

    public class DocuSignTemplate
    {
        /// <summary>
        /// The template's DocuSign identifier (UUID)
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// The template's display name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The template's Message
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The template's DocuSign folder identifier (UUID)
        /// </summary>
        public string FolderId { get; set; }

        /// <summary>
        /// The template's folder name
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// The name of the file for the template's document.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The Uri path to the template's document.
        /// </summary>
        public string AbsoluteUri { get; set; }

        /// <summary>
        /// The MIME type for the template's document.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// The document type for the template. E.g., Word, PDF, etc.
        /// </summary>
        public string TemplateType { get; set; }

        /// <summary>
        /// The date and time the template was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The date and time the template was last modified
        /// </summary>
        public DateTime LastModifiedDate { get; set; }
    }

    public class DocuSignTemplateRecipient
    {
        /// <summary>
        /// The recipient's Template Role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// The recipient's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The recipient's email address
        /// </summary>
        public string Email { get; set; }
    }
}
