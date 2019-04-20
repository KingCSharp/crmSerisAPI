using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace crmSeries.Core.Dtos
{
    public class AddLeadDto
    {
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Cell { get; set; }
        public string Comments { get; set; }
        public string Address2 { get; set; }
        public string CompanyPhone { get; set; }
        public string Web { get; set; }
        public string Fax { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
    }
}
