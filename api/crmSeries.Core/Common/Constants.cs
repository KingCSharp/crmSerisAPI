using System;

namespace crmSeries.Core.Common
{
    public static class Constants
	{
	    public static class DateTimes
	    {
	        public static DateTime MinimumAllowedDateTime => new DateTime(1900, 1, 1);
        }

        public static class IdClaimTypes
		{
			public const string Subject = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
			public const string ObjectId = "http://schemas.microsoft.com/identity/claims/objectidentifier";

			public const string LinkObject = "LinkObject";
		}

		public static class EmailTemplates
		{
			public const string AccountActivate = "Account/Activate";
		    public const string SupportDiagnostics = "Support/Diagnostics";
		}

	    public static class Roles
	    {
	        public const string User = "user";
	        public const string Admin = "admin";
	    }
	}
}
