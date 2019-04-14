using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Branch
    {
        public int BranchId { get; set; }
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
        public bool Deleted { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
