using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace crmSeries.Core.Dtos
{
    public class AddLeadDto
    {
        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(50)]
        public string County { get; set; }

        [StringLength(25)]
        public string Country { get; set; }

        [StringLength(20)]
        public string Cell { get; set; }

        public string Comments { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string CompanyPhone { get; set; }

        [StringLength(200)]
        public string Web { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(10)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Position { get; set; }

        [StringLength(100)]
        public string Department { get; set; }
    }
}
