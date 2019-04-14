using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CompanyAssignedCategory
    {
        public int AssignedId { get; set; }
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
    }
}
