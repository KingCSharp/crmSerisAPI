using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class MessageFile
    {
        public int FileId { get; set; }
        public int MessageId { get; set; }
        public string NylasId { get; set; }
        public string ContentDisposition { get; set; }
        public string ContentId { get; set; }
        public string ContentType { get; set; }
        public string Filename { get; set; }
        public int Size { get; set; }
    }
}
