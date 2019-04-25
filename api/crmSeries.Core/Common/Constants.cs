using System;

namespace crmSeries.Core.Common
{
    public static class Constants
	{
        public static class Auth
        {
            public const string ApiKey = "api-key";
            public const string ApiKeyPolicy = "api-key-policy";
            public const string UnauthorizedApiKey = "The api-key is unauthorized for this API.";
        }

        public static class DateTimes
	    {
	        public static DateTime MinimumAllowedDateTime => new DateTime(1900, 1, 1);
        }

        public static class RegExPatterns
        {
            public const string Ssn = "(?=\\d{5})\\d";
            public const string SsnMask = "x";
        }
    }
}
