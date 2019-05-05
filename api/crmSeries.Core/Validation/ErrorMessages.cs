using crmSeries.Core.Extensions;

namespace crmSeries.Core.Validation
{
    public static class ErrorMessages
    {
        public static string DecimalLocationIncorrect(string propertyName, int allowedDecimalPrecision)
        {
            return $"{propertyName.SplitWords()} must have a decimal precision less than or equal to {allowedDecimalPrecision}";
        }

        public static string SuspectedIncorrect(string propertyName) =>
            $"'{propertyName.SplitWords()}' suspected to be incorrect.";

        public static string MustNotBeNull(string propertyName)
        {
            return $"'{propertyName.SplitWords()}' must not be null.";
        }

        public static string EntityNotFound(string entityName, string property)
        {
            return $"No '{entityName.SplitWords()}' with this '{property.SplitWords()}' was found.";
        }

        public static string EntityNotFound<TEntity>()
        {
            return $"'{typeof(TEntity).Name.SplitWords()}' was not found.";
        }

        public static string EntityNotFound<TEntity>(int id)
        {
            return $"'{typeof(TEntity).Name.SplitWords()}' with ID {id} was not found.";
        }

        public static string EntityNotFound<TEntity>(string property)
        {
            return $"No '{typeof(TEntity).Name.SplitWords()}' with this {property.SplitWords()} was found.";
        }

        public static string EntityMustBeUnique<TEntity>(string uniqueFieldName)
        {
            return $"A '{typeof(TEntity).Name.SplitWords()}' with this {uniqueFieldName} already exists.";
        }

        public static string ReferenceIsRequired<TEntity>()
        {
            return $"A '{typeof(TEntity).Name.SplitWords()}' is required.";
        }

        public static string ReferenceMustExist<TEntity>()
        {
            return $"The '{typeof(TEntity).Name.SplitWords()}' being referenced does not exist.";
        }

        public static string AllReferencesMustExist<TEntity>()
        {
            return $"A '{typeof(TEntity).Name.SplitWords()}' being referenced does not exist.";
        }

        public static string ParentDoesNotContainChild<TParent, TChild>()
        {
            return $"The '{typeof(TParent).Name.SplitWords()}' requested does not reference this '{typeof(TChild).Name.SplitWords()}'.";
        }

        public static class DefaultReplication
        {
            public static string ForNotNull(string field)
            {
                return $"'{field.SplitWords()}' must not be empty.";
            }

            public static string ForNotEmpty(string field)
            {
                return $"'{field.SplitWords()}' should not be empty.";
            }

            public static string ForLength(string field, int min, int max, int entered)
            {
                return $"'{field}' must be between {min} and {max} characters. You entered {entered} characters.";
            }

            public static string ForMaxLength(string field, int max, int entered)
            {
                return $"The length of '{field}' must be {max} characters or fewer. You entered {entered} characters.";
            }

            public static string ForBlobMaxLength(string field, int max, int entered)
            {
                return $"The size of '{field}' must be {max} bytes or fewer. {entered} bytes were provided.";
            }

            public static string ForExactLength(string field, int characters, int entered)
            {
                return $"'{field}' must be {characters} characters in length. You entered {entered} characters.";
            }

            public static string ForGreaterThanOrEqualTo(string field, int value)
            {
                return $"'{field.SplitWords()}' must be greater than or equal to '{value}'.";
            }

            public static string ForInvalidEnum(string field, int value)
            {
                return $"'{field}' has a range of values which does not include '{value}'.";
            }

            public static string ForGreaterThan(string field, int value)
            {
                return $"'{field}' must be greater than '{value}'.";
            }
        }

        public static class Blob
        {
            public static string MaximumLength(byte[] provided, int maxLength)
                => $"The size of '{{PropertyName}}' must be {maxLength} bytes or fewer. " +
                   $"{provided.Length} bytes were provided.";
        }

        public static class Format
        {
            public const string InvalidZipCode = "Zip code must be 5 or 9 digits.";
        }

        public static class Accounts
        {
            public const string AccountAlreadyLinked = "This account has already been activated.";

            public const string BadLinkToken =
                "The provided activation token is invalid. Contact your administrator to have a new one issued.";

            public const string BadIdentity = "The provided identity cannot be used to activate this account.";
        }

        public static class Email
        {
            public const string InvalidSender = "The From field is invalid.";
        }

        public static class UserClaims
        {
            public const string InvalidClaim = "An invalid claim was found in this operation.";
        }

        public static class Users
        {
            public const string IdIsRequired = "A user ID is required.";

            public const string InvalidEmailAddress = "The email address is invalid.";
        }

        public static class WebServices
        {
            public const string WebServiceFailed = "An external service failed during this request.";
        }

        public static class Dates
        {
            public static string InvalidDate(string propertyName)
            {
                return $"'{propertyName}' has a combination of month/day/year that is an invalid date.";
            }
        }

        public static class Leads
        {
            public const string EmailAddressInvalid = "The Email field is invalid.";
            public const string PhoneInvalid = "The Phone field is invalid.";
            public const string CellInvalid = "The Cell field is invalid.";
            public const string FaxInvalid = "The Fax field is invalid.";
            public const string CompanyPhoneInvalid = "The Company Phone field is invalid.";
            public const string PhoneOrEmailRequired = "You must submit either phone number or E-Mail.";
        }

        public static class API
        {
            public const string APIConnectionNotFound = "No connection exists for the provided API key.";
        }

        public static class ExecuteWorkflowRuleRequest
        {
            public const string ModuleInvalid = "The Module provided is not valid.";
            public const string ActionTypeInvalid = "The Action Type provided is not valid";
        }
    }
}