﻿using crmSeries.Core.Domain.HeavyEquipment;

namespace crmSeries.Core.Security
{
    public class ApiUser
    {
        public string DealerName { get; set; }

        public string DatabaseConnectionString { get; set; }

        public int DealerId { get; set; }
    }

    public class IdentityUser
    {
        public int UserId { get; set; }
    }
}