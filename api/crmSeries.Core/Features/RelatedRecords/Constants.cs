using System.Collections.Generic;

namespace crmSeries.Core.Features.RelatedRecords
{
    public static class Constants
    {
        public static class ErrorMessages
        {
            public static readonly string InvalidRecordType = 
                $"The related record type must be one of the following values: ${string.Join(",", RelatedRecord.Types.ValidTypes)}";
        }

        public static class RelatedRecord
        {
            public static class Types
            {
                public const string Company = "Company";
                public const string Contact = "Contact";
                public const string Equipment = "Equipment";
                public const string Lead = "Lead";
                public const string Note = "Note";
                public const string Opportunity = "Opportunity";
                public const string Task = "Task";
                public const string User = "User";

                public static readonly IReadOnlyCollection<string> ValidTypes = new List<string>
                {
                    Company,
                    Contact,
                    Equipment,
                    Lead,
                    Note,
                    Opportunity,
                    Task,
                    User
                };
            }
        }
    }
}
