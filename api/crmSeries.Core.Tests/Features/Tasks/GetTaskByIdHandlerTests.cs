using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks;
using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Mediator;
using NSubstitute;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Tasks
{
    [TestFixture]
    public class GetTaskByIdHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssues_ReturnsTaskById()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                context.Task.Add(new Task { TaskId = 1 });
                context.SaveChanges();

                var getRelatedRecordNameHandler =
                    Substitute.For<IRequestHandler<GetRelatedRecordNameRequest, string>>();

                getRelatedRecordNameHandler.HandleAsync(Arg.Any<GetRelatedRecordNameRequest>())
                    .Returns("foo".AsResponseAsync());

                var handler = new GetTaskByIdHandler(context, getRelatedRecordNameHandler);

                // Act
                var response = handler.HandleAsync(new GetTaskByIdRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TaskId, 1);
            }
        }

        [Test]
        public void HandleAsync_NoTaskFound_ReturnsEmptyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var getRelatedRecordNameHandler =
                    Substitute.For<IRequestHandler<GetRelatedRecordNameRequest, string>>();

                getRelatedRecordNameHandler.HandleAsync(Arg.Any<GetRelatedRecordNameRequest>())
                    .Returns("foo".AsResponseAsync());

                var handler = new GetTaskByIdHandler(context, getRelatedRecordNameHandler);

                // Act
                var response = handler.HandleAsync(new GetTaskByIdRequest(1));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage, TasksConstants.ErrorMessages.TaskNotFound);
                Assert.IsNull(response.Result.Data);
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordIsTypeCompany_ReturnsTaskWithRelatedRecordNameSetToCompanyName()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var company = new Company
                {
                    CompanyId = 1,
                    CompanyName = "Nubitz, LLC"
                };
                context.Company.Add(company);

                var task = new Task
                {
                    TaskId = 1,
                    RelatedRecordType = Constants.RelatedRecord.Types.Company,
                    RelatedRecordId = company.CompanyId
                };
                context.Task.Add(task);

                context.SaveChanges();

                var getRelatedRecordNameHandler = new GetRelatedRecordNameHandler(context);
                var handler = new GetTaskByIdHandler(context, getRelatedRecordNameHandler);
                
                // Act
                var response = handler.HandleAsync(new GetTaskByIdRequest(task.TaskId));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(company.CompanyName, response.Result.Data.RelatedRecordName);
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordIsTypeContact_ReturnsTaskWithRelatedRecordNameSetToContactFirstNameAndLastNameConcatenated()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var contact = new Contact
                {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = "Smith"
                };
                context.Contact.Add(contact);

                var task = new Task
                {
                    TaskId = 1,
                    RelatedRecordType = Constants.RelatedRecord.Types.Contact,
                    RelatedRecordId = contact.ContactId
                };
                context.Task.Add(task);

                context.SaveChanges();

                var getRelatedRecordNameHandler = new GetRelatedRecordNameHandler(context);
                var handler = new GetTaskByIdHandler(context, getRelatedRecordNameHandler);

                // Act
                var response = handler.HandleAsync(new GetTaskByIdRequest(task.TaskId));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual($"{contact.FirstName} {contact.LastName}", response.Result.Data.RelatedRecordName);
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordIsTypeLead_ReturnsTaskWithRelatedRecordNameSetToLeadFirstNameAndLastNameConcatenated()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var lead = new Lead
                {
                    LeadId = 1,
                    FirstName = "John",
                    LastName = "Smith"
                };
                context.Lead.Add(lead);

                var task = new Task
                {
                    TaskId = 1,
                    RelatedRecordType = Constants.RelatedRecord.Types.Lead,
                    RelatedRecordId = lead.LeadId
                };
                context.Task.Add(task);

                context.SaveChanges();

                var getRelatedRecordNameHandler = new GetRelatedRecordNameHandler(context);
                var handler = new GetTaskByIdHandler(context, getRelatedRecordNameHandler);

                // Act
                var response = handler.HandleAsync(new GetTaskByIdRequest(task.TaskId));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual($"{lead.FirstName} {lead.LastName}", response.Result.Data.RelatedRecordName);
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordIsTypeCompanyButItDoesNotExist_ReturnsTaskWithRelatedRecordNameSetToNull()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var company = new Company
                {
                    CompanyId = 1,
                    CompanyName = "Foo Enterprises"
                };
                context.Company.Add(company);

                var task = new Task
                {
                    TaskId = 1,
                    RelatedRecordType = Constants.RelatedRecord.Types.Company,
                    RelatedRecordId = company.CompanyId + 1
                };
                context.Task.Add(task);

                context.SaveChanges();

                var getRelatedRecordNameHandler = new GetRelatedRecordNameHandler(context);
                var handler = new GetTaskByIdHandler(context, getRelatedRecordNameHandler);

                // Act
                var response = handler.HandleAsync(new GetTaskByIdRequest(task.TaskId));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.IsNull(response.Result.Data.RelatedRecordName);
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordIsTypeContactButItDoesNotExist_ReturnsTaskWithRelatedRecordNameSetToNull()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var contact = new Contact
                {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = "Smith"
                };
                context.Contact.Add(contact);

                var task = new Task
                {
                    TaskId = 1,
                    RelatedRecordType = Constants.RelatedRecord.Types.Contact,
                    RelatedRecordId = contact.ContactId + 1
                };
                context.Task.Add(task);

                context.SaveChanges();

                var getRelatedRecordNameHandler = new GetRelatedRecordNameHandler(context);
                var handler = new GetTaskByIdHandler(context, getRelatedRecordNameHandler);

                // Act
                var response = handler.HandleAsync(new GetTaskByIdRequest(task.TaskId));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.IsNull(response.Result.Data.RelatedRecordName);
            }
        }

        [Test]
        public void HandleAsync_RelatedRecordIsTypeLeadButItDoesNotExist_ReturnsTaskWithRelatedRecordNameSetToNull()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var lead = new Lead
                {
                    LeadId = 1,
                    FirstName = "John",
                    LastName = "Smith"
                };
                context.Lead.Add(lead);

                var task = new Task
                {
                    TaskId = 1,
                    RelatedRecordType = Constants.RelatedRecord.Types.Lead,
                    RelatedRecordId = lead.LeadId + 1
                };
                context.Task.Add(task);

                context.SaveChanges();

                var getRelatedRecordNameHandler = new GetRelatedRecordNameHandler(context);
                var handler = new GetTaskByIdHandler(context, getRelatedRecordNameHandler);

                // Act
                var response = handler.HandleAsync(new GetTaskByIdRequest(task.TaskId));

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.IsNull(response.Result.Data.RelatedRecordName);
            }
        }
    }
}