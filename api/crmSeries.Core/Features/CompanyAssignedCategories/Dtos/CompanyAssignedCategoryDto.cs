using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.CompanyAssignedCategories.Dtos
{
    public class BaseCompanyAssignedCategoryDto
    {
        /// <summary>
        /// The uninque identifier for the company this category is for.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// The uninque identifier for the category.
        /// </summary>
        public int CategoryId { get; set; }
    }

    public class GetCompanyAssignedCategoryDto : BaseCompanyAssignedCategoryDto
    {
        /// <summary>
        /// The unique identifier for this company assigned category entity.
        /// </summary>
        public int AssignedId { get; set; }

        /// <summary>
        /// The name of the category.
        /// </summary>
        public string Category { get; set; }
    }

    public class EditCompanyAssignedCategoryDto : BaseCompanyAssignedCategoryDto
    {
        /// <summary>
        /// The unique identifier for this company assigned category entity.
        /// </summary>
        public int AssignedId { get; set; }
    }

    public class AddCompanyAssignedCategoryDto : BaseCompanyAssignedCategoryDto
    {

    }
}
