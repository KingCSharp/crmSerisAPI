using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class EquipmentUcreadingMeasurement
    {
        public int MeasurementId { get; set; }
        public int ReadingId { get; set; }
        public string Component { get; set; }
        public string Location { get; set; }
        public string Method { get; set; }
        public decimal Measurement { get; set; }
        public decimal PercentWorn { get; set; }
        public int CriteriaId { get; set; }
    }
}
