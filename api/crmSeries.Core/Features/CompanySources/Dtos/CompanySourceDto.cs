using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.CompanySources.Dtos
{
    public class BaseCompanySourceDto
    {
        /// <summary>
        /// The name of the company source.
        /// </summary>
        public string Source { get; set; }
    }

    public class GetCompanySourceDto : BaseCompanySourceDto
    {
        /// <summary>
        /// The unnique identifier of the company source.
        /// </summary>
        public int SourceId { get; set; }
    }

    public class AddCompanySourceDto : BaseCompanySourceDto
    {
    }

    public class EditCompanySourceDto : BaseCompanySourceDto
    {
        /// <summary>
        /// The unnique identifier of the company source.
        /// </summary>
        public int SourceId { get; set; }
    }
}
