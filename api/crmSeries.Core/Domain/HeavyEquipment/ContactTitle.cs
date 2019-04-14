using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class ContactTitle
    {
        public int TitleId { get; set; }
        public string Title { get; set; }
        public bool Deleted { get; set; }
    }
}
