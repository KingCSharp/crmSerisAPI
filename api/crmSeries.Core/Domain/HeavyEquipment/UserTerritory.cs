using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserTerritory
    {
        public int TerritoryId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string TerritoryCriteria { get; set; }
        public string TerritoryRules { get; set; }
    }
}
