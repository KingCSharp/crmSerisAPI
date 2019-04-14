using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class NotePurposeAlert
    {
        public int AlertId { get; set; }
        public int PurposeId { get; set; }
        public string RecordType { get; set; }
        public int RecordId { get; set; }
    }
}
