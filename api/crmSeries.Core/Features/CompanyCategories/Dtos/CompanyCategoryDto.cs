using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.CompanyCategories.Dtos
{
    public class BaseCompanyCategoryDto
    {
        /// <summary>
        /// The company category's name.
        /// </summary>
        public string Category { get; set; }
    }

    public class GetCompanyCategoryDto : BaseCompanyCategoryDto
    {
        /// <summary>
        /// The unnique identifier of the company category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Soft delete flag for the company category.
        /// </summary>
        public bool Deleted { get; set; }
    }

    public class AddCompanyCategoryDto : BaseCompanyCategoryDto
    {
    }

    public class EditCompanyCategoryDto : BaseCompanyCategoryDto
    {
        /// <summary>
        /// The unnique identifier of the company category.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
