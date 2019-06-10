using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.CompanyRanks.Dtos
{
    public class BaseCompanyRankDto
    {
        /// <summary>
        /// The name of the company rank.
        /// </summary>
        public string Rank { get; set; }
    }

    public class GetCompanyRankDto : BaseCompanyRankDto
    {
        /// <summary>
        /// The unnique identifier of the company rank.
        /// </summary>
        public int RankId { get; set; }

        /// <summary>
        /// Soft delete flag for the company rank.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Flagged true if this company rank is the default prospect.
        /// </summary>
        public bool DefaultProspect { get; set; }
    }

    public class AddCompanyRankDto : BaseCompanyRankDto
    {
    }

    public class EditCompanyRankDto : BaseCompanyRankDto
    {
        /// <summary>
        /// The unnique identifier of the company rank.
        /// </summary>
        public int RankId { get; set; }
    }
}
