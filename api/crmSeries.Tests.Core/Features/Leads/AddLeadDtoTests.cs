using crmSeries.Core.Features.Leads.Dtos;
using NUnit.Framework;

namespace crmSeries.Tests.Core.Features.Leads
{
    [TestFixture]
    public class AddLeadDtoTests
    {
        [TestCase("John Doe", "John", "Doe")]
        [TestCase("John    Doe", "John", "Doe")]
        [TestCase("John Doe Doe", "John", "Doe Doe")]
        [TestCase("John Doe    Doe", "John", "Doe Doe")]
        [TestCase("John Doe    Doe    Doe", "John", "Doe Doe Doe")]
        [TestCase("John", "John", null)]
        [TestCase("John ", "John", null)]
        [TestCase("John   ", "John", null)]
        public void NameProperties_MapsProperly(string fullname, string firstname, string lastname)
        {
            // Arrange
            var dto = new AddLeadDto
            {
                Name = fullname
            };

            // Act

            // Assert
            Assert.AreEqual(firstname, dto.FirstName);
            Assert.AreEqual(lastname, dto.LastName);
        }
    }
}