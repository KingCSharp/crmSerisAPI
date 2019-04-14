using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class InspectionItem
    {
        public int ItemId { get; set; }
        public int GroupId { get; set; }
        public string Item { get; set; }
        public int Sequence { get; set; }
        public string DataType { get; set; }
        public bool RequireResponse { get; set; }
        public bool RequireImage { get; set; }
        public bool RequireComment { get; set; }
        public string RequirementFilter { get; set; }
    }
}
