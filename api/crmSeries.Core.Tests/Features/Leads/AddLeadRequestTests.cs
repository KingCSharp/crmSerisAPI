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
                CompanyName = "Foo Enterprises",
                Email = "foo@bar.com",
                Name = "John Doe",
                Cell = "5042555599",
                Fax = "5042559999",
                Phone = "5042559999",
                CompanyPhone = "5042599755",
                Description = "Sample Description",
                Department = "Sample Department",
                Comments = "Sample comments",
                Address1 = "123 Main St.",
                Address2 = "Suite 101",
                Position = "Manager",
                State = "LA",
                Title = "Sample Title",
                Web = "http://nubitz.io",
                Zip = "70816",
                City = "Denham Springs",
                County = "Livingston",
                Country = "USA",
            };
        }

        [Test]
        public void Handle_NormalRequest_LeadIsAddedWithAppropriateData()
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
                Assert.AreEqual(lead.Address1, addLeadRequest.Address1);
                Assert.AreEqual(lead.Address2, addLeadRequest.Address2);
                Assert.AreEqual(lead.Cell, addLeadRequest.Cell);
                Assert.AreEqual(lead.City, addLeadRequest.City);
                Assert.AreEqual(lead.Comments, addLeadRequest.Comments);
                Assert.AreEqual(lead.CompanyName, addLeadRequest.CompanyName);
                Assert.AreEqual(lead.CompanyPhone, addLeadRequest.CompanyPhone);
                Assert.AreEqual(lead.Country, addLeadRequest.Country);
                Assert.AreEqual(lead.County, addLeadRequest.County);
                Assert.AreEqual(lead.Department, addLeadRequest.Department);
                Assert.AreEqual(lead.Description, addLeadRequest.Description);
                Assert.AreEqual(lead.Email, addLeadRequest.Email);
                Assert.AreEqual(lead.Fax, addLeadRequest.Fax);
                Assert.AreEqual(lead.FirstName, addLeadRequest.FirstName);
                Assert.AreEqual(lead.LastName, addLeadRequest.LastName);
                Assert.AreEqual(lead.Phone, addLeadRequest.Phone);
                Assert.AreEqual(lead.Position, addLeadRequest.Position);
                Assert.AreEqual(lead.State, addLeadRequest.State);
                Assert.AreEqual(lead.Title, addLeadRequest.Title);
                Assert.AreEqual(lead.Web, addLeadRequest.Web);
                Assert.AreEqual(lead.Zip, addLeadRequest.Zip);
                Assert.AreEqual(lead.OwnerId, userId);
            }
        }

        [Test]
        public void Handle_OwnerEmailIsIncluded_LeadIsAddedWithAppropriateOwnerIdAndAssignedDate()
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
                Assert.IsNotNull(lead.DateAssigned);
            }
        }

        [Test]
        public void Handle_OwnerEmailIsIncludedButDoesNotExist_LeadIsAddedWithOwnerIdOfZero()
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
                    Email = "foo@bar.com",
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
                Assert.AreEqual(lead.OwnerId, 0);
            }
        }

        [Test]
        public void HandleAsync_DefaultLeadStatusExists_StatusIdIsSetToDefaultId()
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

                context.LeadStatus.Add(new LeadStatus
                {
                    StatusId = 1,
                    Status = "The Status",
                    InternalStatus = "The Internal Status",
                    Deleted = false,
                    DefaultNew = true
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
                Assert.AreEqual(1, lead.StatusId);
            }
        }

        [Test]
        public void HandleAsync_DefaultLeadStatusDoesNotExist_StatusIdSetToZero()
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
                Assert.AreEqual(0, lead.StatusId);
            }
        }

        [Test]
        public void HandleAsync_DefaultLeadStatusExistsButIsDeleted_StatusIdSetToZero()
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

                context.LeadStatus.Add(new LeadStatus
                {
                    StatusId = 1,
                    Status = "The Status",
                    InternalStatus = "The Internal Status",
                    Deleted = true,
                    DefaultNew = true
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
                Assert.AreEqual(0, lead.StatusId);
            }
        }
    }
}