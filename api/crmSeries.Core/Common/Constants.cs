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
            public const string Email = "email";
            public const string NoUser = "No users found for this request.";
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

        public static class ErrorMessages
        {
            public const string EmailAddressInvalid = "The Email field is invalid.";
            public const string PhoneInvalid = "The Phone field is invalid.";
            public const string CellInvalid = "The Cell field is invalid.";
            public const string FaxInvalid = "The Fax field is invalid.";
            public const string CompanyPhoneInvalid = "The Company Phone field is invalid.";
            public const string PhoneOrEmailRequired = "You must submit either phone number or E-Mail.";
        }

        public static class Emails
        {
            public static class Leads
            {
                public const string DealerNameKey = "{DealerName}";
            }
        }

        public static class UserFavoriteRecords
        {
            public static class Types
            {
                public const string Company = "Company";
            }
        }
    }
}
