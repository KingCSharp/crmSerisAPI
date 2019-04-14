using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class InspectionResponse
    {
        public int ResponseId { get; set; }
        public int ItemId { get; set; }
        public string Response { get; set; }
        public int Sequence { get; set; }
        public bool RequireImage { get; set; }
    }
}
