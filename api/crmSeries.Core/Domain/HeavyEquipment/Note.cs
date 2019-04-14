using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class Note
    {
        public int NoteId { get; set; }
        public int RecordId { get; set; }
        public string RecordType { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public DateTime NoteDate { get; set; }
        public string Comments { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Deleted { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
