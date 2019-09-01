using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Notes
{
    [TestFixture]
    public class GetNotesForCompanyRequestHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NotesAssociatedWithCompany_ReturnsNoteAssociatedWithCompany()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var companyId = 1;
                var company = new Company { CompanyId = 1 };
                context.Company.Add(company);
                context.SaveChanges();

                var companyNotes = new List<Note> {
                    new Note { NoteId = 1, RecordId = companyId, RecordType = "Company" },
                    new Note { NoteId = 2, RecordId = companyId, RecordType = "Company" },
                    new Note { NoteId = 3, RecordId = companyId, RecordType = "Company" },
                    new Note { NoteId = 4, RecordId = companyId, RecordType = "Company" },
                };
                context.Note.AddRange(companyNotes);

                var nonCompanyNotes = new List<Note> {
                    new Note { NoteId = 5, RecordId = companyId + 1, RecordType = "Company" },
                    new Note { NoteId = 6, RecordId = companyId + 1, RecordType = "Company" },
                    new Note { NoteId = 7, RecordId = companyId + 1, RecordType = "Company" },
                    new Note { NoteId = 8, RecordId = companyId + 1, RecordType = "Company" },
                };
                context.Note.AddRange(nonCompanyNotes);
                context.SaveChanges();

                var verificationHandler = Substitute.For<IRequestHandler<VerifyRelatedRecordRequest>>();
                verificationHandler.HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                    .Returns(Response.SuccessAsync());

                var handler = new GetNotesForCompanyHandler(
                    context, verificationHandler);

                var request = new GetNotesForCompanyRequest(companyId, 1, 5);

                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, companyNotes.Count());
                Assert.IsTrue(response.Result.Data.Items.Select(x => x.NoteId).All(x => x <= 4));
            }
        }

        [Test]
        public void HandleAsync_NotesAssociatedWithContactOfCompany_ReturnsNoteAssociatedWithCompany()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var companyId = 1;
                var company = new Company { CompanyId = 1 };
                context.Company.Add(company);
                context.SaveChanges();

                var contactId = 4;
                var contact = new Contact
                {
                    ContactId = contactId,
                    CompanyId = companyId
                };
                context.Contact.Add(contact);
                context.SaveChanges();

                var companyNotes = new List<Note> {
                    new Note { NoteId = 1, RecordId = contactId, RecordType = "Contact" },
                    new Note { NoteId = 2, RecordId = contactId, RecordType = "Contact" },
                    new Note { NoteId = 3, RecordId = contactId, RecordType = "Contact" },
                    new Note { NoteId = 4, RecordId = contactId, RecordType = "Contact" },
                };
                context.Note.AddRange(companyNotes);
                context.SaveChanges();

                var nonCompanyNotes = new List<Note> {
                    new Note { NoteId = 5, RecordId = companyId + 1, RecordType = "Company" },
                    new Note { NoteId = 6, RecordId = companyId + 1, RecordType = "Company" },
                    new Note { NoteId = 7, RecordId = companyId + 1, RecordType = "Company" },
                    new Note { NoteId = 8, RecordId = companyId + 1, RecordType = "Company" },
                };
                context.Note.AddRange(nonCompanyNotes);
                context.SaveChanges();

                var verificationHandler = Substitute.For<IRequestHandler<VerifyRelatedRecordRequest>>();
                verificationHandler.HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                    .Returns(Response.SuccessAsync());

                var handler = new GetNotesForCompanyHandler(
                    context, verificationHandler);

                var request = new GetNotesForCompanyRequest(companyId, 1, 5);

                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.AreEqual(companyNotes.Count(), response.Result.Data.TotalItemCount);
                Assert.IsNotNull(response.Result.Data);
                Assert.IsTrue(response.Result.Data.Items.Select(x => x.NoteId).All(x => x <= 4));
            }
        }

        [Test]
        public void HandleAsync_NotesAssociatedWithContactOfCompanyAndCompany_ReturnsNoteAssociatedWithCompanyAndContactsOfCompany()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var companyId = 1;
                var company = new Company { CompanyId = 1 };
                context.Company.Add(company);
                context.SaveChanges();

                var contactId = 4;
                var contact = new Contact
                {
                    ContactId = contactId,
                    CompanyId = companyId
                };
                context.Contact.Add(contact);
                context.SaveChanges();

                var companyNotes = new List<Note> {
                    new Note { NoteId = 1, RecordId = contactId, RecordType = "Contact" },
                    new Note { NoteId = 2, RecordId = contactId, RecordType = "Contact" },
                    new Note { NoteId = 3, RecordId = contactId, RecordType = "Contact" },
                    new Note { NoteId = 4, RecordId = contactId, RecordType = "Contact" },
                    new Note { NoteId = 5, RecordId = companyId, RecordType = "Company" },
                    new Note { NoteId = 6, RecordId = companyId, RecordType = "Company" },
                    new Note { NoteId = 7, RecordId = companyId, RecordType = "Company" },
                    new Note { NoteId = 8, RecordId = companyId, RecordType = "Company" },
                };
                context.Note.AddRange(companyNotes);
                context.SaveChanges();

                var verificationHandler = Substitute.For<IRequestHandler<VerifyRelatedRecordRequest>>();
                verificationHandler.HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                    .Returns(Response.SuccessAsync());

                var handler = new GetNotesForCompanyHandler(
                    context, verificationHandler);

                var request = new GetNotesForCompanyRequest(companyId, 1, 5);

                // Act
                var response = handler.HandleAsync(request);

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                Assert.AreEqual(companyNotes.Count(), response.Result.Data.TotalItemCount);
                Assert.IsNotNull(response.Result.Data);
                Assert.IsTrue(response.Result.Data.Items.Select(x => x.NoteId).All(x => x <= 8));
            }
        }
    }
}