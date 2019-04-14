using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class RecordAssignedFile
    {
        public int AssignedId { get; set; }
        public int RecordId { get; set; }
        public string RecordType { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string AbsoluteUri { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public DateTimeOffset? Uploaded { get; set; }
    }
}
