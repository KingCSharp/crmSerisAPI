﻿using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CompanyCategory
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public bool Deleted { get; set; }
    }
}
