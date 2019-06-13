using crmSeries.Core.Features.CompanyAssignedCategories.Dtos;
using crmSeries.Core.Features.CompanyAssignedRanks.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.Companies.Dtos
{
    public class BaseCompanyDto
    {
        /// <summary>
        /// The parent node identifier.
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// The record type identifier.
        /// </summary>
        public int RecordTypeId { get; set; }

        /// <summary>
        /// The company branch identifier.
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// The company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The company's legal name.
        /// </summary>
        public string LegalName { get; set; }

        /// <summary>
        /// The account number for the company.
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// The company's address line 1.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// The company's address line 2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// The company's address line 3.
        /// </summary>
        public string Address3 { get; set; }

        /// <summary>
        /// The city the company is in.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The city the state is in.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The zip code the company's address is in.
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// The county/parish the company is in.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// The country the company is in.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Whether the company accepts mail.
        /// </summary>
        public bool? Mailing { get; set; }

        /// <summary>
        /// The latitude of the company's location.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// The longitude of the company's location.
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// The company's phone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The company's fax number.
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// The company's website domain.
        /// </summary>
        public string Web { get; set; }

        /// <summary>
        /// Whether the company is linked or not.
        /// </summary>
        public bool Linked { get; set; }

        /// <summary>
        /// The source id of the company. E.g., Cold Call, Website, Email Marketing, etc.
        /// </summary>
        public int SourceId { get; set; }

        /// <summary>
        /// The status of the company. E.g., Account, Prospect, etc.
        /// </summary>
        public string Status { get; set; }
    }

    public class GetCompanyDto : BaseCompanyDto
    {
        /// <summary>
        /// The company identifier.
        /// </summary>
        public int CompanyId { get; set; }        

        /// <summary>
        /// Flag for soft deletion.
        /// </summary>
        public bool Deleted { get; set; }        

        /// <summary>
        /// The date that the company information was last modified.
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Flag for if this company record is set as a favorite by the user.
        /// </summary>
        public bool Favorite { get; set; }

        /// <summary>
        /// The name of the branch associated with this company.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// The source of the company. E.g., Cold Call, Website, Email Marketing, etc.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// List of ranks for the company.
        /// </summary>
        public List<GetCompanyAssignedRankDto> Ranks { get; set; }

        /// <summary>
        /// List of categories for the company.
        /// </summary>
        public List<GetCompanyAssignedCategoryDto> Categories { get; set; }

        /// <summary>
        /// The type of company. E.g., Corporate, Mine, Quarry, etc.
        /// </summary>
        public string RecordType { get; set; }

        /// <summary>
        /// The name of the parent company that this company belongs to.
        /// </summary>
        public string ParentName { get; set; }
    }

    public class EditCompanyDto : BaseCompanyDto
    {
        /// <summary>
        /// The company identifier.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Flag for soft deletion.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// List of IDs for CompanyCategories associated with this company
        /// </summary>
        public List<int> Categories { get; set; }

        /// <summary>
        /// List of IDs for CompanyRanks and UserRoles associated with this company
        /// </summary>
        public List<BaseAddCompanyAssignedRankDto> Ranks { get; set; }
    }

    public class AddCompanyDto : BaseCompanyDto
    {
        /// <summary>
        /// List of IDs for CompanyCategories associated with this company
        /// </summary>
        public List<int> Categories { get; set; }

        /// <summary>
        /// List of IDs for CompanyRanks and UserRoles associated with this company
        /// </summary>
        public List<BaseAddCompanyAssignedRankDto> Ranks { get; set; }
    }
}
