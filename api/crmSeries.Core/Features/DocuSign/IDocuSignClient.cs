using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.DocuSign
{
    public interface IDocuSignClient
    {
        Task<List<DocuSignTemplate>> GetTemplates();

        Task SendTemplate(string templateId, IEnumerable<DocuSignTemplateRecipient> recipients);
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
