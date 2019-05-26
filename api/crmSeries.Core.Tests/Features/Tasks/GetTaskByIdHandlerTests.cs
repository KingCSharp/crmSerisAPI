using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Tasks;
using crmSeries.Core.Features.Tasks.Utility;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Tasks
{
    [TestFixture]
    public class GetTaskByIdHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_ReturnsTaskById()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Task.Add(new Task { TaskId = 1 });
                context.SaveChanges();

                var handler = new GetTaskByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetTaskByIdRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TaskId, 1);
            }
        }

        [Test]
        public void NormalRequest_NoTaskFound_ReturnsEmptyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new GetTaskByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetTaskByIdRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage, TasksConstants.ErrorMessages.TaskNotFound);
                Assert.IsNull(response.Result.Data);
            }
        }
    }
}