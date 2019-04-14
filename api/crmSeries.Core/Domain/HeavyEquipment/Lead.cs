using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Lead
    {
        public int LeadId { get; set; }
        public int OwnerId { get; set; }
        public int StatusId { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public bool Deleted { get; set; }
        public int SourceId { get; set; }
        public string Comments { get; set; }
        public string Address2 { get; set; }
        public string CompanyPhone { get; set; }
        public string Web { get; set; }
        public string Fax { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public DateTimeOffset? DateAssigned { get; set; }
        public DateTimeOffset? DateAcknowledged { get; set; }
        public DateTimeOffset? DateConverted { get; set; }
    }
}
