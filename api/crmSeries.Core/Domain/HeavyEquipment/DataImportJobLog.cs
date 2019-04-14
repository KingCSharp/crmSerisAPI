using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DataImportJobLog
    {
        public int LogId { get; set; }
        public int JobId { get; set; }
        public int RecordIndex { get; set; }
        public string Result { get; set; }
        public string Error { get; set; }
    }
}
