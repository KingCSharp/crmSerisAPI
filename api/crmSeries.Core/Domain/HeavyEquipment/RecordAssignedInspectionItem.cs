using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RecordAssignedInspectionItem
    {
        public int AssignedItemId { get; set; }
        public int AssignedGroupId { get; set; }
        public string Item { get; set; }
        public int Sequence { get; set; }
        public string DataType { get; set; }
        public string Response { get; set; }
        public string Comments { get; set; }
        public bool RequireResponse { get; set; }
        public bool RequireImage { get; set; }
        public bool RequireComment { get; set; }
        public string RequirementFilter { get; set; }
        public decimal ReconditionAmount { get; set; }
    }
}
