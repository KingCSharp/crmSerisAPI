using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class SystemLanguage
    {
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public string Display { get; set; }
        public string ShortDateFormat { get; set; }
        public string LongDateFormat { get; set; }
        public string ShortTimeFormat { get; set; }
        public string LongTimeFormat { get; set; }
        public string FullDateTimeFormat { get; set; }
        public string TimestampFormat { get; set; }
    }
}
