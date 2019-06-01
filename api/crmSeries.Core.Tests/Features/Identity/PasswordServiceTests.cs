using System.Collections.Generic;
using System.Linq;
using crmSeries.Core.Security;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Identity
{
    [TestFixture]
    public class PasswordServiceTests
    {
        [TestCase(10)]
        public void CreateHash_CalledWithSameArguments_YieldsSameResult(int times)
        {
            // Arrange
            var service = new PasswordService();

            // Act
            var results = new List<string>();

            for (int i = 0; i < times; ++i)
                results.Add(service.CreateHash("password", "salt"));

            // Assert
            Assert.AreEqual(1, results.Distinct().Count());
        }

        [Test]
        public void CreateHash_WithDifferentPasswords_ShouldYieldDifferentResults()
        {
            // Arrange
            var service = new PasswordService();

            // Act
            var result1 = service.CreateHash("password1", "salt");
            var result2 = service.CreateHash("password2", "salt");

            // Assert
            Assert.AreNotEqual(result1, result2);
        }

        [Test]
        public void CreateHash_WithDifferentSalts_ShouldYieldDifferentResults()
        {
            // Arrange
            var service = new PasswordService();

            // Act
            var result1 = service.CreateHash("password", "salt1");
            var result2 = service.CreateHash("password", "salt2");

            // Assert
            Assert.AreNotEqual(result1, result2);
        }
    }
}
