using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EmailTemplate
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public bool Deleted { get; set; }

        public bool Internal { get; set; }
    }
}
