using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Features.Notes.Utility;
using NUnit.Framework;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Notes
{
    [TestFixture]
    public class GetNoteByIdHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssues_ReturnsNoteById()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Note.Add(new Note { NoteId = 1 });
                context.SaveChanges();

                var handler = new GetNoteByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetNoteByIdRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.NoteId, 1);
            }
        }

        [Test]
        public void HandleAsync_NoNoteFound_ReturnsEmptyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new GetNoteByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetNoteByIdRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage, NotesConstants.ErrorMessages.NoteNotFound);
                Assert.IsNull(response.Result.Data);
            }
        }
    }
}