using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Features.Notes.Utility;
using NUnit.Framework;
using System;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Notes
{
    [TestFixture]
    public class DeleteNoteHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssues_NoteDeletedSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Note.Add(new Note
                {
                    NoteId = 1,
                    Comments = "Test Comments",
                    NoteDate = DateTime.Now
                });
                context.SaveChanges();

                var handler = new DeleteNoteHandler(context);

                // Act
                var response = handler.HandleAsync(new DeleteNoteRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var note = context.Note.SingleOrDefault(x => x.NoteId == 1);
                Assert.IsNotNull(note);
                Assert.AreEqual(note.Deleted, true);
            }
        }

        [Test]
        public void HandleAsync_NoteNotFound_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new DeleteNoteHandler(context);

                // Act
                var response = handler.HandleAsync(new DeleteNoteRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    NotesConstants.ErrorMessages.NoteNotFound);
            }
        }
    }
}