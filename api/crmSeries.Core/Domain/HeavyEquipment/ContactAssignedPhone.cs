using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class ContactAssignedPhone
    {
        public int PhoneId { get; set; }
        public int ContactId { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public bool Deleted { get; set; }
    }
}
