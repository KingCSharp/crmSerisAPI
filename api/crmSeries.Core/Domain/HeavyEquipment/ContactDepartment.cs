using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class ContactDepartment
    {
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public bool Deleted { get; set; }
    }
}
