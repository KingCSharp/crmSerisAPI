using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.Equipment.Utility;
using crmSeries.Core.Features.Leads.Utility;
using crmSeries.Core.Features.Notes.Utility;
using crmSeries.Core.Features.Opportunities.Utility;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Features.Users.Utility;
using NUnit.Framework;
using System;
using System.Linq;
using static crmSeries.Core.Features.RelatedRecords.Constants;

namespace crmSeries.Core.Tests.Features.RelatedRecords
{
    [TestFixture]
    public class VerifyRelatedRecordHandlerTests : BaseUnitTest
    {
        [TestCase(RelatedRecord.Types.Company)]
        [TestCase(RelatedRecord.Types.Contact)]
        [TestCase(RelatedRecord.Types.Equipment)]
        [TestCase(RelatedRecord.Types.Lead)]
        [TestCase(RelatedRecord.Types.Note)]
        [TestCase(RelatedRecord.Types.Opportunity)]
        [TestCase(RelatedRecord.Types.Task)]
        [TestCase(RelatedRecord.Types.User)]
        public void NormalRequest_NoIssues_RelatedTypeFound(string type)
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                AddAllSupportedRecordTypes(context);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new VerifyRelatedRecordHandler(context);

                // Act
                var response = handler.HandleAsync(new VerifyRelatedRecordRequest
                {
                    RecordType = type,
                    RecordTypeId = 1
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
            }
        }

        [TestCase(RelatedRecord.Types.Company, CompaniesConstants.ErrorMessages.CompanyNotFound)]
        [TestCase(RelatedRecord.Types.Contact, ContactsConstants.ErrorMessages.ContactNotFound)]
        [TestCase(RelatedRecord.Types.Equipment, EquipmentConstants.ErrorMessages.EquipmentNotFound)]
        [TestCase(RelatedRecord.Types.Lead, LeadsConstants.ErrorMessages.InvalidLead)]
        [TestCase(RelatedRecord.Types.Note, NotesConstants.ErrorMessages.NoteNotFound)]
        [TestCase(RelatedRecord.Types.Opportunity, OpportunitiesConstants.ErrorMessages.OpportunityNotFound)]
        [TestCase(RelatedRecord.Types.Task, TasksConstants.ErrorMessages.TaskNotFound)]
        [TestCase(RelatedRecord.Types.User, UsersConstants.ErrorMessages.UserNotFound)]
        public void NormalRequest_NoRecordFound_ReturnsAppropriateMessage(string type, string errorMessage)
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new VerifyRelatedRecordHandler(context);

                // Act
                var response = handler.HandleAsync(new VerifyRelatedRecordRequest
                {
                    RecordType = type,
                    RecordTypeId = 1
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage, errorMessage);
            }
        }

        [Test]
        public void NormalRequest_IdSetToZero_ReturnsSuccess()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new VerifyRelatedRecordHandler(context);

                // Act
                var response = handler.HandleAsync(new VerifyRelatedRecordRequest
                {
                    RecordType = "Foo Bar",
                    RecordTypeId = 0
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
            }
        }

        private void AddAllSupportedRecordTypes(HeavyEquipmentContext context)
        {
            context.Company.Add(new Company { CompanyId = 1 });
            context.Contact.Add(new Contact { ContactId = 1 });
            context.Equipment.Add(new Equipment { EquipmentId = 1 });
            context.Lead.Add(new Lead { LeadId = 1 });
            context.Note.Add(new Note { NoteId = 1 });
            context.Opportunity.Add(new Opportunity { OpportunityId = 1 });
            context.Task.Add(new Task { TaskId = 1 });
            context.User.Add(new User { UserId = 1 });
            context.SaveChanges();
        }
    }
}