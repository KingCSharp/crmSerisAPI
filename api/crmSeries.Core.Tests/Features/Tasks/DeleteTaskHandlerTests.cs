using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Features.Notes.Utility;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks;
using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Features.Users.Utility;
using NUnit.Framework;
using System;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Tasks
{
    [TestFixture]
    public class DeleteTaskHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_TaskDeletedSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Task.Add(new Task
                {
                    TaskId = 1,
                    Comments = "Test Comments",
                });
                context.SaveChanges();

                var handler = new DeleteTaskHandler(context);

                // Act
                var response = handler.HandleAsync(new DeleteTaskRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var task = context.Task.SingleOrDefault(x => x.TaskId == 1);
                Assert.IsNotNull(task);
                Assert.AreEqual(task.Deleted, true);
            }
        }

        [Test]
        public void NormalRequest_TaskNotFound_ReturnsAppropriateError()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new DeleteTaskHandler(context);

                // Act
                var response = handler.HandleAsync(new DeleteTaskRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    TasksConstants.ErrorMessages.TaskNotFound);
            }
        }
    }
}