namespace crmSeries.Core.Tests
{
    public class ExpectedError
    {
        public static string ForGreaterThan(string field, decimal value)
        {
            return $"'{field}' must be greater than '{value}'.";
        }

        public static string ForLessThanOrEqualTo(string field, decimal value)
        {
            return $"'{field}' must be less than or equal to '{value}'.";
        }
    }
}