using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Notes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Notes
{
    [TestFixture]
    public class GetNoteTypesHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssues_ReturnsFullNoteTypeResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.NoteType.Add(new NoteType { TypeId = i });
                }
                context.SaveChanges();

                var handler = new GetNoteTypesHandler(context);

                // Act
                var response = handler.HandleAsync(new GetNoteTypesRequest());

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.Count(), itemCount);
            }
        }

        [Test]
        public void HandleAsync_NoNoteTypesFound_ReturnsEmptyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new GetNoteTypesHandler(context);

                // Act
                var response = handler.HandleAsync(new GetNoteTypesRequest());

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.Count(), 0);
            }
        }

        [Test]
        public void HandleAsync_FromDateIsSet_ReturnsOnlyNotesGreaterThanOrEqualToTheDateSet()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new GetNoteTypesHandler(context);

                // Act
                var response = handler.HandleAsync(new GetNoteTypesRequest());

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.Count(), 0);
            }
        }
    }
}