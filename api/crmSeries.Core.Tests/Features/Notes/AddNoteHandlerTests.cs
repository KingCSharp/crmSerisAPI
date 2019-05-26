﻿using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.Users.Utility;
using NUnit.Framework;
using System;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Notes
{
    [TestFixture]
    public class AddNoteHandlerTests : BaseUnitTest
    {
        [Test]
        public void NormalRequest_NoIssues_NoteAddedSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var handler = new AddNoteHandler(context, verificationHandler);

                // Act
                var response = handler.HandleAsync(new AddNoteRequest
                {
                    Comments = "Test Comments",
                    Latitude = 0,
                    Longitude = 0,
                    NoteDate = DateTime.Now,
                    RecordId = 0,
                    RecordType = Constants.RelatedRecord.Types.Contact,
                    RecordTypeId = 0,
                    UserId = 0
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var note = context.Note.SingleOrDefault(x => x.NoteId == 1);
                Assert.IsNotNull(note);
                Assert.AreEqual(note.Comments, "Test Comments");
                Assert.AreEqual(note.RecordType, Constants.RelatedRecord.Types.Contact);
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
                var handler = new AddNoteHandler(context, verificationHandler);

                // Act
                var response = handler.HandleAsync(new AddNoteRequest
                {
                    Comments = "Test Comments",
                    Latitude = 0,
                    Longitude = 0,
                    NoteDate = DateTime.Now,
                    RecordId = 0,
                    RecordType = Constants.RelatedRecord.Types.Contact,
                    RecordTypeId = 1,
                    UserId = 0
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage, 
                    ContactsConstants.ErrorMessages.ContactNotFound);
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
                var handler = new AddNoteHandler(context, verificationHandler);

                // Act
                var response = handler.HandleAsync(new AddNoteRequest
                {
                    Comments = "Test Comments",
                    Latitude = 0,
                    Longitude = 0,
                    NoteDate = DateTime.Now,
                    RecordId = 0,
                    RecordType = Constants.RelatedRecord.Types.Contact,
                    RecordTypeId = 0,
                    UserId = 1
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, true);
                Assert.AreEqual(response.Result.Errors[0].ErrorMessage,
                    UsersConstants.ErrorMessages.UserNotFound);
            }
        }
    }
}