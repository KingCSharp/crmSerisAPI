using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RecordAssignedInspectionItemResponse
    {
        public int ResponseId { get; set; }
        public int AssignedItemId { get; set; }
        public string Response { get; set; }
        public int Sequence { get; set; }
    }
}
