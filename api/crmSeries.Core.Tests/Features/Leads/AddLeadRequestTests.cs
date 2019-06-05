using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Leads;
using crmSeries.Core.Features.Workflows;
using crmSeries.Core.Mediator;
using NSubstitute;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Leads
{
    [TestFixture]
    public class AddLeadHandlerTests : BaseUnitTest
    {
        private AddLeadRequest addLeadRequest;

        [SetUp]
        public void Setup()
        {
            addLeadRequest = new AddLeadRequest
            {
                Email = "foo@bar.com",
                Name = "John Doe",
            };
        }

        [Test]
        public void Handle_OwnerEmailIsIncluded_LeadIsAddedWithAppropriateOwnerId()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            var leadId = 0;
            var userId = 33;
            var userEmail = "john@smith-industries.com";

            using (var context = new HeavyEquipmentContext(options))
            {
                context.User.Add(new User
                {
                    UserId = userId,
                    Email = userEmail,
                    Active = true,
                    Deleted = false
                });
                context.SaveChanges();

                var executeWorkflowHandler =
                    Substitute.For<IRequestHandler<ExecuteWorkflowRuleRequest, ExecuteWorkflowResponse>>();


                executeWorkflowHandler.HandleAsync(Arg.Any<ExecuteWorkflowRuleRequest>())
                    .Returns(new ExecuteWorkflowResponse().AsResponseAsync());

                var addLeadAuditHandler =
                    Substitute.For<IRequestHandler<AddLeadAuditRequest>>();

                var handler = new AddLeadRequestHandler(context, executeWorkflowHandler, addLeadAuditHandler);

                addLeadRequest.OwnerEmail = userEmail; 

                // Act
                var response = handler.HandleAsync(addLeadRequest);
                leadId = response.Result.Data.Id;

                var lead = context.Lead.SingleOrDefault(x => x.LeadId == leadId);

                //Assert 
                Assert.IsNotNull(lead);
                Assert.AreEqual(lead.OwnerId, userId);
            }
        }
    }
}