using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RequiredFieldOption
    {
        public int RequiredId { get; set; }
        public string RecordType { get; set; }
        public string Field { get; set; }
        public bool Complete { get; set; }
        public bool Required { get; set; }
        public bool ForceUpperCase { get; set; }
        public bool DisableLinked { get; set; }
    }
}
