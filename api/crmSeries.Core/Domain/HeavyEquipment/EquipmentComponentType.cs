﻿using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentComponentType
    {
        public int TypeId { get; set; }
        public string Type { get; set; }
        public bool Deleted { get; set; }
    }
}
