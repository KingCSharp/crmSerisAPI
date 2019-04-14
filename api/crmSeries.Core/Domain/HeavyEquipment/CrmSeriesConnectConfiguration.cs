using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.HeavyEquipment
{
    public partial class CrmSeriesConnectConfiguration
    {
        public int ConfigId { get; set; }
        public bool FtpSecure { get; set; }
        public string FtpHost { get; set; }
        public string FtpUserName { get; set; }
        public string FtpPassword { get; set; }
        public string ConnectionString { get; set; }
        public string UploadType { get; set; }
        public string BlobContainer { get; set; }
    }
}
