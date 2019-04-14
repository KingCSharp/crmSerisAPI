using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class DataImportJob
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string ImportType { get; set; }
        public string FileLocation { get; set; }
        public string SourceFields { get; set; }
        public string DataFields { get; set; }
        public string Action { get; set; }
        public string MatchField { get; set; }
        public bool AddLookup { get; set; }
        public bool IgnoreEmpty { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public int Added { get; set; }
        public int Updated { get; set; }
        public int Skipped { get; set; }
    }
}
