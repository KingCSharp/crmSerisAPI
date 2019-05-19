using System;
using System.Collections.Generic;
using System.Text;

namespace crmSeries.Core.Features.Companies.Dtos
{
    public class CompanyDto
    {

        /// <summary>
        /// The company identifier.
        /// </summary>
        public int CompanyId { get; set; }

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
        /// Flag for soft deletion.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// The source of the company. E.g., Cold Call, Website, Email Marketing, etc.
        /// </summary>
        public int SourceId { get; set; }

        /// <summary>
        /// The status of the company. E.g., Account, Prospect, etc.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The date that the company information was last modified.
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Flag for if this company record is set as a favorite by the user.
        /// </summary>
        public bool Favorite { get; set; }
    }
}
