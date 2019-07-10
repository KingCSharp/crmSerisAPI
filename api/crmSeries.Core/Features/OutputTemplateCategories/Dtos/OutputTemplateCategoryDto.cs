using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.OutputTemplateCategories.Dtos
{
    public class BaseOutputTemplateCategoryDto
    {
        /// <summary>
        /// The output template category's name.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The type of records this template category uses. E.g., Deal
        /// </summary>
        public string RecordType { get; set; }
    }

    public class GetOutputTemplateCategoryDto : BaseOutputTemplateCategoryDto
    {
        /// <summary>
        /// The unnique identifier of the output template category.
        /// </summary>
        public int CategoryId { get; set; }
    }

    public class AddOutputTemplateCategoryDto : BaseOutputTemplateCategoryDto
    {
    }

    public class EditOutputTemplateCategoryDto : BaseOutputTemplateCategoryDto
    {
        /// <summary>
        /// The unnique identifier of the output template category.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
