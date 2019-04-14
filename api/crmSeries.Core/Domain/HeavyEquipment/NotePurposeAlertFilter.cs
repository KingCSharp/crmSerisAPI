using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class NotePurposeAlertFilter
    {
        public int AssignedId { get; set; }
        public int AlertId { get; set; }
        public string RecordType { get; set; }
        public int RecordId { get; set; }
    }
}
