using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.RelatedRecords;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Features.Tasks.Dtos;

namespace crmSeries.Core.Tests.Features.Contacts
{
    [TestFixture]
    public class GetFullContactByIdHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssues_ReturnsContact()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var company = new Company
                {
                    CompanyId = 1,
                    CompanyName = "Foo Company"
                };

                context.Company.Add(company);

                var contact = new Contact
                {
                    ContactId = 1,
                    CompanyId = 1
                };

                context.Contact.Add(contact);

                context.SaveChanges();
                var handler = new GetFullContactByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetFullContactByIdRequest(1));

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(contact.ContactId, response.Result.Data.Details.ContactId);
                Assert.AreEqual(company.CompanyName, response.Result.Data.Details.CompanyName);
            }
        }

        [Test]
        public void HandleAsync_NoIssues_ReturnsContactWithEmptyRelatedRecords()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var company = new Company
                {
                    CompanyId = 1
                };
                context.Company.Add(company);

                var contact = new Contact
                {
                    ContactId = 1,
                    CompanyId = 1
                };

                context.Contact.Add(contact);
                context.SaveChanges();

                var handler = new GetFullContactByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetFullContactByIdRequest(1));

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(contact.ContactId, response.Result.Data.Details.ContactId);
                Assert.AreEqual(new List<GetNoteDto>(), response.Result.Data.Notes);
                Assert.AreEqual(new List<GetTaskDto>(), response.Result.Data.Tasks);
            }
        }

        [Test]
        public void HandleAsync_NoIssuesRelatedNotesFound_ReturnsContactWithRelatedNotes()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var company = new Company
                {
                    CompanyId = 1,
                    CompanyName = "Foo Company"
                };
                context.Company.Add(company);

                var contact = new Contact
                {
                    ContactId = 1,
                    CompanyId = 1
                };
                context.Contact.Add(contact);

                var notes = new List<Note>
                {
                    new Note
                    {
                        NoteId = 1,
                        RecordId  = contact.ContactId,
                        RecordType = Constants.RelatedRecord.Types.Contact
                    },
                    new Note
                    {
                        NoteId = 2,
                        RecordId  = contact.ContactId,
                        RecordType = Constants.RelatedRecord.Types.Contact
                    }
                };
                context.Note.AddRange(notes);

                context.SaveChanges();

                var handler = new GetFullContactByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetFullContactByIdRequest(1));

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(contact.ContactId, response.Result.Data.Details.ContactId);
                Assert.AreEqual(notes[0].NoteId, response.Result.Data.Notes.ToList()[0].NoteId);
                Assert.AreEqual(notes[1].NoteId, response.Result.Data.Notes.ToList()[1].NoteId);
            }
        }

        [Test]
        public void HandleAsync_NoIssuesRelatedTasksFound_ReturnsTasksWithRelatedNotes()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var company = new Company
                {
                    CompanyId = 1,
                    CompanyName = "Foo Company"
                };
                context.Company.Add(company);

                var contact = new Contact
                {
                    ContactId = 1,
                    CompanyId = 1
                };
                context.Contact.Add(contact);

                var tasks = new List<Task>
                {
                    new Task
                    {
                        TaskId = 1,
                        ContactId = contact.ContactId,
                    },
                    new Task
                    {
                        TaskId = 2,
                        ContactId = contact.ContactId
                    }
                };
                context.Task.AddRange(tasks);

                context.SaveChanges();

                var handler = new GetFullContactByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetFullContactByIdRequest(1));

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(contact.ContactId, response.Result.Data.Details.ContactId);
                Assert.AreEqual(tasks[0].TaskId, response.Result.Data.Tasks.ToList()[0].TaskId);
                Assert.AreEqual(tasks[1].TaskId, response.Result.Data.Tasks.ToList()[1].TaskId);
            }
        }

        [Test]
        public void HandleAsync_NoContactFound_ReturnsEmptyData()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new GetFullContactByIdHandler(context);

                // Act
                var response = handler.HandleAsync(new GetFullContactByIdRequest(1));

                //Assert 
                Assert.AreEqual(true, response.Result.HasErrors);
                Assert.IsNull(response.Result.Data);
                Assert.AreEqual(ContactsConstants.ErrorMessages.ContactNotFound, response.Result.Errors[0].ErrorMessage);
            }
        }
    }
}