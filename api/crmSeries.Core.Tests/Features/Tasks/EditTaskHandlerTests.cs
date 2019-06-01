using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.Notes.Utility;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks;
using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Features.Users.Utility;
using NUnit.Framework;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Tasks
{
    [TestFixture]
    public class EditTaskHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_TaskEditedSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Task.Add(new Task
                {
                    TaskId = 1,
                    Comments = "Test Comments",
                    RelatedRecordType = Constants.RelatedRecord.Types.Note
                });
                context.SaveChanges();
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                using (var verificationContext = new HeavyEquipmentContext(options))
                {
                    var verificationHandler = new VerifyRelatedRecordHandler(verificationContext);
                    var handler = new EditTaskHandler(context, verificationHandler);

                    // Act
                    var response = handler.HandleAsync(new EditTaskRequest
                    {
                        TaskId = 1,
                        Comments = "Test Comments Edited",
                        RelatedRecordType = Constants.RelatedRecord.Types.Note
                    });

                    //Assert 
                    Assert.AreEqual(response.Result.HasErrors, false);
                }
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var task = context.Task.SingleOrDefault(x => x.TaskId == 1);
                Assert.IsNotNull(task);
                Assert.AreEqual(task.Comments, "Test Comments Edited");
                Assert.AreEqual(task.RelatedRecordType, Constants.RelatedRecord.Types.Note);
            }
        }

        [Test]
        public void NormalRequest_NoRelatedRecord_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new EditTaskHandler(context, verificationHandler);

                // Act
                var response = handler.HandleAsync(new EditTaskRequest
                {
                    TaskId = 0,
                    Comments = "Test Comments Edited",
                    RelatedRecordId = 1,
                    RelatedRecordType = Constants.RelatedRecord.Types.Note,
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    NotesConstants.ErrorMessages.NoteNotFound);
            }
        }

        [Test]
        public void NormalRequest_NoRelatedUser_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new EditTaskHandler(context, verificationHandler);

                // Act
                var response = handler.HandleAsync(new EditTaskRequest
                {
                    TaskId = 0,
                    Comments = "Test Comments Edited",
                    RelatedRecordType = Constants.RelatedRecord.Types.Note,
                    UserId = 1
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    UsersConstants.ErrorMessages.UserNotFound);
            }
        }

        [Test]
        public void NormalRequest_NoRelatedContact_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new EditTaskHandler(context, verificationHandler);

                // Act
                var response = handler.HandleAsync(new EditTaskRequest
                {
                    TaskId = 0,
                    Comments = "Test Comments Edited",
                    RelatedRecordType = Constants.RelatedRecord.Types.Note,
                    ContactId = 1
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    ContactsConstants.ErrorMessages.ContactNotFound);
            }
        }

        [Test]
        public void NormalRequest_NoRelatedTask_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new EditTaskHandler(context, verificationHandler);

                // Act
                var response = handler.HandleAsync(new EditTaskRequest
                {
                    TaskId = 1,
                    Comments = "Test Comments Edited",
                    RelatedRecordType = Constants.RelatedRecord.Types.Note
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    TasksConstants.ErrorMessages.TaskNotFound);
            }
        }
    }
}