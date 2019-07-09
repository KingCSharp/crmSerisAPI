using System;
using DocuSign.eSign.Client.Auth;

namespace crmSeries.Core.Features.DocuSign.Utility
{
    public class DocuSignContext
    {
        public OAuth.UserInfo.Account Account { get; set; }

        public DocuSignToken AccessToken { get; set; }
    }

    public class DocuSignToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
