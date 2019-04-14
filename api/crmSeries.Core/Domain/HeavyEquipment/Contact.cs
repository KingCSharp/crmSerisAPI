using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Contact
    {
        public int ContactId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
