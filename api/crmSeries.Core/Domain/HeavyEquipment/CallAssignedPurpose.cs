using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CallAssignedPurpose
    {
        public int AssignedId { get; set; }
        public int CallId { get; set; }
        public int PurposeId { get; set; }
    }
}
