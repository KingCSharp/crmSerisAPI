using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.CompanyAssignedCategories.Dtos
{
    public class BaseCompanyAssignedCategoryDto
    {
        public int CompanyId { get; set; }

        public int CategoryId { get; set; }
    }

    public class GetCompanyAssignedCategoryDto : BaseCompanyAssignedCategoryDto
    {
        public int AssignedId { get; set; }

        public string Category { get; set; }
    }

    public class EditCompanyAssignedCategoryDto : BaseCompanyAssignedCategoryDto
    {
        public int AssignedId { get; set; }
    }

    public class AddCompanyAssignedCategoryDto : BaseCompanyAssignedCategoryDto
    {

    }
}
