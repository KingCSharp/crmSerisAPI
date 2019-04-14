using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentSmuhistory
    {
        public int HistoryId { get; set; }
        public int EquipmentId { get; set; }
        public string UpdateType { get; set; }
        public decimal Hours { get; set; }
        public DateTime HistoryDate { get; set; }
        public string ActionBy { get; set; }
        public DateTime ActionDate { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string SatelliteProvider { get; set; }
        public DateTime? LastGpsreading { get; set; }
        public DateTime? LastGpsoperation { get; set; }
        public string LastLocation { get; set; }
    }
}
