using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class NotePurpose
    {
        public int PurposeId { get; set; }
        public string Purpose { get; set; }
        public string Abbreviation { get; set; }
        public bool Deleted { get; set; }
    }
}
