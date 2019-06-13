using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.CompanyAssignedRanks.Dtos
{
    public class BaseCompanyAssignedRankDto
    {
        public int CompanyId { get; set; }

        public int RankId { get; set; }

        public int RoleId { get; set; }
    }

    public class GetCompanyAssignedRankDto : BaseCompanyAssignedRankDto
    {
        public int AssignedId { get; set; }

        public string CompanyName { get; set; }

        public string Rank { get; set; }

        public string Role { get; set; }
    }

    public class EditCompanyAssignedRankDto : BaseCompanyAssignedRankDto
    {
        public int AssignedId { get; set; }
    }

    public class BaseAddCompanyAssignedRankDto
    {
        public int RankId { get; set; }

        public int RoleId { get; set; }
    }

    public class AddCompanyAssignedRankDto : BaseAddCompanyAssignedRankDto
    {
        public int CompanyId { get; set; }
    }
}