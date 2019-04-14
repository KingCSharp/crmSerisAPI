using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class InspectionGroup
    {
        public int GroupId { get; set; }
        public int InspectionId { get; set; }
        public string GroupName { get; set; }
        public int Sequence { get; set; }
    }
}
