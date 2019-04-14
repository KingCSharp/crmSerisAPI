using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserLocation
    {
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public string Location { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public bool MailMerge { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
