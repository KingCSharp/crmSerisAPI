using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CompanyRank
    {
        public int RankId { get; set; }
        public string Rank { get; set; }
        public bool Deleted { get; set; }
        public bool DefaultProspect { get; set; }
    }
}
