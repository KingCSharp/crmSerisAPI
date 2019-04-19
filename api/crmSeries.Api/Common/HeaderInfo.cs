using System.Linq;
using crmSeries.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace crmSeries.Api.Common
{
    public static class HeaderInfo
    {
        public static string GetAPIKey(HttpRequest request)
        {
            StringValues apiKey;
            request.Headers.TryGetValue(Constants.Auth.ApiKey, out apiKey);
            return Enumerable.FirstOrDefault<string>(apiKey) ?? "";
        }
    }
}
