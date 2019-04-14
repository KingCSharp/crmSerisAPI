using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DealUserAccess
    {
        public int AccessId { get; set; }
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public bool ViewDeal { get; set; }
        public bool EditDeal { get; set; }
        public bool ReassignTask { get; set; }
        public bool Global { get; set; }
        public string RecordSelection { get; set; }
        public bool Active { get; set; }
    }
}
