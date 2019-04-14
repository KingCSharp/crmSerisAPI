using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserType
    {
        public int TypeId { get; set; }
        public string UserType1 { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }
    }
}
