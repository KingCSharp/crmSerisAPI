using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class NoteAssignedPurpose
    {
        public int AssignedId { get; set; }
        public int NoteId { get; set; }
        public int PurposeId { get; set; }
    }
}
