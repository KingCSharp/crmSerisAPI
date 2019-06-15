using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.CompanyAssignedRanks.Dtos
{
    public class BaseCompanyAssignedRankDto
    {
        /// <summary>
        /// The uninque identifier for the company this rank is for.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// The uninque identifier for the company rank.
        /// </summary>
        public int RankId { get; set; }

        /// <summary>
        /// The uninque identifier for the user role. 
        /// </summary>
        public int RoleId { get; set; }
    }

    public class GetCompanyAssignedRankDto : BaseCompanyAssignedRankDto
    {
        /// <summary>
        /// The unique identifier for this company assigned rank entity.
        /// </summary>
        public int AssignedId { get; set; }

        /// <summary>
        /// The name of the company.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The name of the company rank.
        /// </summary>
        public string Rank { get; set; }

        /// <summary>
        /// The name of the user role.
        /// </summary>
        public string Role { get; set; }
    }

    public class EditCompanyAssignedRankDto : BaseCompanyAssignedRankDto
    {
        /// <summary>
        /// The unique identifier for this company assigned rank entity.
        /// </summary>
        public int AssignedId { get; set; }
    }

    public class BaseAddCompanyAssignedRankDto
    {
        /// <summary>
        /// The uninque identifier for the company rank.
        /// </summary>
        public int RankId { get; set; }

        /// <summary>
        /// The uninque identifier for the user role. 
        /// </summary>
        public int RoleId { get; set; }
    }

    public class AddCompanyAssignedRankDto : BaseAddCompanyAssignedRankDto
    {
        /// <summary>
        /// The unique identifier for this company assigned rank entity.
        /// </summary>
        public int CompanyId { get; set; }
    }
}