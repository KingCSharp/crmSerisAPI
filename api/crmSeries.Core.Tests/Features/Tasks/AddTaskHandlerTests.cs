using crmSeries.Core.Data;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.Notes.Utility;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks;
using crmSeries.Core.Features.Users.Utility;
using NUnit.Framework;
using System.Linq;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Security;
using NSubstitute;

namespace crmSeries.Core.Tests.Features.Tasks
{
    [TestFixture]
    public class AddTaskHandlerTests : BaseUnitTest
    {
        private IIdentityUserContext GetIdentityUserContext()
        {
            var identityContext =
                Substitute.For<IIdentityUserContext>();

            identityContext.RequestingUser.Returns(new IdentityUser
            {
                UserId = 1
            });

            return identityContext;
        }

        [Test]
        public void HandleAsync_NoIssues_TaskAddedSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new AddTaskHandler(context, verificationHandler, GetIdentityUserContext());

                // Act
                var response = handler.HandleAsync(new AddTaskRequest
                {
                    Comments = "Test Comments",
                    RelatedRecordType = Constants.RelatedRecord.Types.Note
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var task = context.Task.SingleOrDefault(x => x.TaskId == 1);
                Assert.IsNotNull(task);
                Assert.AreEqual(task.Comments, "Test Comments");
                Assert.AreEqual(task.RelatedRecordType, Constants.RelatedRecord.Types.Note);
            }
        }

        [Test]
        public void HandleAsync_NoRelatedRecord_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new AddTaskHandler(context, verificationHandler, GetIdentityUserContext());

                // Act
                var response = handler.HandleAsync(new AddTaskRequest
                {
                    Comments = "Test Comments",
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
        public void HandleAsync_NoRelatedUser_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new AddTaskHandler(context, verificationHandler, GetIdentityUserContext());

                // Act
                var response = handler.HandleAsync(new AddTaskRequest
                {
                    Comments = "Test Comments",
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
        public void HandleAsync_NoRelatedContact_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new AddTaskHandler(context, verificationHandler, GetIdentityUserContext());

                // Act
                var response = handler.HandleAsync(new AddTaskRequest
                {
                    Comments = "Test Comments",
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
        public void HandleAsync_UserIdIsNotPassed_TaskIsAssignedToLoggedInUser()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new AddTaskHandler(context, verificationHandler, GetIdentityUserContext());

                // Act
                var response = handler.HandleAsync(new AddTaskRequest
                {
                    Comments = "Test Comments",
                    RelatedRecordType = Constants.RelatedRecord.Types.Note
                });

                var task = context.Task
                    .Where(x => x.UserId == GetIdentityUserContext().RequestingUser.UserId);

                //Assert 
                Assert.IsNotNull(task);
            }
        }

        [Test]
        public void HandleAsync_UserIdIsPassed_TaskIsAssignedToPassedInUserId()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new AddTaskHandler(context, verificationHandler, GetIdentityUserContext());

                var userId = 3;

                context.User.Add(new User
                {
                    UserId = userId
                });

                // Act
                var response = handler.HandleAsync(new AddTaskRequest
                {
                    Comments = "Test Comments",
                    UserId = userId,
                    RelatedRecordType = Constants.RelatedRecord.Types.Note
                });

                var task = context.Task
                    .Where(x => x.UserId == userId);

                //Assert 
                Assert.IsNotNull(task);
            }
        }
    }
}