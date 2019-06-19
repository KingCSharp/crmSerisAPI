using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Features.Notes.Utility;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Users.Utility;
using NUnit.Framework;
using System.Linq;
using static crmSeries.Core.Features.RelatedRecords.Constants;

namespace crmSeries.Core.Tests.Features.Notes
{
    [TestFixture]
    public class EditNoteHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssues_NoteEditedSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Note.Add(new Note
                {
                    NoteId = 1,
                    Comments = "Test Comments",
                    RecordType = RelatedRecord.Types.Contact
                });
                context.SaveChanges();
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                using (var verificationContext = new HeavyEquipmentContext(options))
                {
                    var verificationHandler = new VerifyRelatedRecordHandler(verificationContext);
                    var handler = new EditNoteHandler(context, verificationHandler);

                    // Act
                    var response = handler.HandleAsync(new EditNoteRequest
                    {
                        NoteId = 1,
                        Comments = "Test Comments Edited",
                        RecordType = RelatedRecord.Types.Contact
                    });

                    //Assert 
                    Assert.AreEqual(response.Result.HasErrors, false);
                }
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var note = context.Note.SingleOrDefault(x => x.NoteId == 1);
                Assert.IsNotNull(note);
                Assert.AreEqual(note.Comments, "Test Comments Edited");
            }
        }

        [Test]
        public void HandleAsync_NoRelatedRecord_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Note.Add(new Note
                {
                    NoteId = 1,
                    Comments = "Test Comments",
                    RecordType = RelatedRecord.Types.Contact
                });
                context.SaveChanges();

                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new EditNoteHandler(context, verificationHandler);

                // Act
                var response = handler.HandleAsync(new EditNoteRequest
                {
                    NoteId = 1,
                    Comments = "Test Comments",
                    RecordType = RelatedRecord.Types.Contact,
                    RecordTypeId = 1
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    ContactsConstants.ErrorMessages.ContactNotFound);
            }
        }

        [Test]
        public void HandleAsync_NoRelatedUser_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Note.Add(new Note
                {
                    NoteId = 1,
                    Comments = "Test Comments",
                    RecordType = RelatedRecord.Types.Contact
                });
                context.SaveChanges();

                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new EditNoteHandler(context, verificationHandler);

                // Act
                var response = handler.HandleAsync(new EditNoteRequest
                {
                    NoteId = 1,
                    Comments = "Test Comments",
                    UserId = 1,
                    RecordType = RelatedRecord.Types.Contact
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    UsersConstants.ErrorMessages.UserNotFound);
            }
        }

        [Test]
        public void HandleAsync_NoNoteFound_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new EditNoteHandler(context, verificationHandler);

                // Act
                var response = handler.HandleAsync(new EditNoteRequest
                {
                    NoteId = 1,
                    Comments = "Test Comments",
                    UserId = 1,
                    RecordType = RelatedRecord.Types.Contact
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    NotesConstants.ErrorMessages.NoteNotFound);
            }
        }
    }
}