using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace crmSeries.Core.Tests
{
    public static class TestUtility
    {
        private static Random random = new Random();

        public static string MaxLengthError(string propertyName, int maxLength)
        {
            return $"The length of '{propertyName}' must be {maxLength} characters or fewer.";
        }

        public static string GenerateStringOfLength(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
