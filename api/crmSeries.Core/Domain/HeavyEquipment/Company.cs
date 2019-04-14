using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Company
    {
        public int CompanyId { get; set; }
        public int ParentId { get; set; }
        public int RecordTypeId { get; set; }
        public int BranchId { get; set; }
        public string CompanyName { get; set; }
        public string LegalName { get; set; }
        public string AccountNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public bool? Mailing { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Web { get; set; }
        public bool Linked { get; set; }
        public bool Deleted { get; set; }
        public int SourceId { get; set; }
        public string Status { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
