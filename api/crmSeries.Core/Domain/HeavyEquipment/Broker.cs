using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Broker
    {
        public int BrokerId { get; set; }
        public string BrokerName { get; set; }
        public string BrokerNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public bool Preferred { get; set; }
        public bool Visible { get; set; }
        public bool Deleted { get; set; }
    }
}
