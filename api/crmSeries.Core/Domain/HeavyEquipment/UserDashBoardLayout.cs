using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class UserDashBoardLayout
    {
        public int AssignedId { get; set; }
        public int UserId { get; set; }
        public string FormName { get; set; }
        public string DashboardName { get; set; }
        public string GridStack { get; set; }
        public bool DefaultLayout { get; set; }
    }
}
