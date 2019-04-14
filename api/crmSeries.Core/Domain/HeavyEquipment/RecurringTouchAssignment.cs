using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RecurringTouchAssignment
    {
        public int AssignmentId { get; set; }
        public int RecordId { get; set; }
        public string RecordType { get; set; }
        public int UserId { get; set; }
        public int ProfileId { get; set; }
        public int ValueId { get; set; }
        public bool Override { get; set; }
        public int OverrideTouchesPerYear { get; set; }
        public DateTime? NextTargetDate { get; set; }
        public bool Deleted { get; set; }
    }
}
