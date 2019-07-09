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
                public const string CompanyCategory = "CompanyCategory";
                public const string CompanyRank = "CompanyRank";
                public const string Contact = "Contact";
                public const string Deal = "Deal";
                public const string Equipment = "Equipment";
                public const string Lead = "Lead";
                public const string Note = "Note";
                public const string Opportunity = "Opportunity";
                public const string OutputTemplate = "OutputTemplate";
                public const string OutputTemplateCategory = "OutputTemplateCategory";
                public const string OutputTemplateField = "OutputTemplateField";
                public const string Rank = "Rank";
                public const string Task = "Task";
                public const string User = "User";
                public const string UserRole = "UserRole";

                public static readonly IReadOnlyCollection<string> ValidTypes = new List<string>
                {
                    Company,
                    CompanyCategory,
                    CompanyRank,                    
                    Contact,
                    Deal,
                    Equipment,
                    Lead,
                    Note,
                    Opportunity,
                    OutputTemplate,
                    OutputTemplateCategory,
                    OutputTemplateField,
                    Rank,
                    Task,
                    User,
                    UserRole
                };
            }
        }
    }
}
