using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserAssignedSalesCode
    {
        public int AssignedId { get; set; }
        public int UserId { get; set; }
        public string SalesCode { get; set; }
    }
}
