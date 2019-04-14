using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class ContactAssignedEmail
    {
        public int EmailId { get; set; }
        public int ContactId { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool Deleted { get; set; }
    }
}
