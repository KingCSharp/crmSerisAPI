using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserGridColumnLayout
    {
        public int AssignedId { get; set; }
        public int UserId { get; set; }
        public string Grid { get; set; }
        public string Columns { get; set; }
    }
}
