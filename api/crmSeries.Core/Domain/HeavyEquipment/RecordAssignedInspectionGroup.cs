using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RecordAssignedInspectionGroup
    {
        public int AssignedGroupId { get; set; }
        public int AssignedInspectionId { get; set; }
        public string GroupName { get; set; }
        public int Sequence { get; set; }
        public string Comments { get; set; }
    }
}
