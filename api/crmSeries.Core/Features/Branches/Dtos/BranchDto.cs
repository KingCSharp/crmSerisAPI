using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.Branches.Dtos
{
    public class BaseBranchDto
    {
        public int DivisionId { get; set; }
        public string BranchName { get; set; }
        public bool Hq { get; set; }
        public string BranchNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Web { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    public class GetBranchDto : BaseBranchDto
    {
        /// <summary>
        /// The unnique identifier of the branch.
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// Soft delete flag for the company rank.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// The timestamp that this branch was created.
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }

    public class AddBranchDto : BaseBranchDto
    {
    }

    public class EditBranchDto : BaseBranchDto
    {
        /// <summary>
        /// The unnique identifier of the branch.
        /// </summary>
        public int BranchId { get; set; }
    }
}
