using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.Branches.Dtos
{
    public class BaseBranchDto
    {
        /// <summary>
        /// The id for this branch's division.
        /// </summary>
        public int DivisionId { get; set; }

        /// <summary>
        /// The name of the branch.
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// Denotes whether this branch is the headquarters.
        /// </summary>
        public bool Hq { get; set; }


        /// <summary>
        /// The branch number.
        /// </summary>
        public string BranchNo { get; set; }

        /// <summary>
        /// The first line of this branch's address.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// The second line of this branch's address.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// The city this branch is located in.
        /// </summary>
        public string City { get; set; }


        /// <summary>
        /// The state this branch is located in.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The branch's zip code.
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The branch's county.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// The country this branch is located in.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The branch's phone number.
        /// </summary>
        public string Phone { get; set; }


        /// <summary>
        /// The branch's fax number.
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// The URL of the branch's website.
        /// </summary>
        public string Web { get; set; }

        /// <summary>
        /// The latitude location of the branch.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// The longitude location of the branch.
        /// </summary>
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
