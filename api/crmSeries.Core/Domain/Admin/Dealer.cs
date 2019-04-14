using System;
using System.Collections.Generic;

namespace crmSeries.Core.Domain.Admin
{
    public partial class Dealer
    {
        public int DealerId { get; set; }
        public string DealerName { get; set; }
        public string Dbstring { get; set; }
        public string Dbname { get; set; }
        public string CommonDbname { get; set; }
        public string CommonDbstring { get; set; }
        public string StorageAccount { get; set; }
        public string StorageAccountKey { get; set; }
        public string Apikey { get; set; }
        public string Apisalt { get; set; }
    }
}
