using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Logic.Queries;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace crmSeries.Core.Tests.Features.Notes
{
    [TestFixture]
    public class GetNotesRequestHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssues_ReturnsNoteResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                int itemCount = 10;
                for (int i = 1; i <= itemCount; ++i)
                {
                    context.Note.Add(new Note { NoteId = i, UserId = user.UserId });
                }
                context.SaveChanges();

                var handler = new GetNotesRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetNotesRequest { PageInfo = query });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, itemCount);
                Assert.AreEqual(response.Result.Data.PageNumber, 1);
                Assert.AreEqual(response.Result.Data.PageCount, itemCount / query.PageSize);
                Assert.AreEqual(response.Result.Data.PageNumber, query.PageNumber);
                Assert.AreEqual(response.Result.Data.PageSize, query.PageSize);
            }
        }

        [Test]
        public void HandleAsync_NoNotesFound_ReturnsEmptyResults()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var handler = new GetNotesRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetNotesRequest { PageInfo = query });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, 0);
                Assert.AreEqual(response.Result.Data.PageNumber, 1);
                Assert.AreEqual(response.Result.Data.PageCount, 0);
                Assert.AreEqual(response.Result.Data.PageNumber, query.PageNumber);
                Assert.AreEqual(response.Result.Data.PageSize, query.PageSize);
            }
        }

        [Test]
        public void HandleAsync_FromDateIsSet_ReturnsNotesFromTheDateRequested()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var notes = new List<Note>
                {
                    new Note {
                        UserId = user.UserId,
                        Comments = "Unmatching criteria",
                        NoteDate = new System.DateTime(2017, 5, 5).ToUniversalTime()
                    },
                    new Note {
                        UserId = user.UserId,
                        Comments = "Matching criteria",
                        NoteDate = new System.DateTime(2018, 5, 5).ToUniversalTime()
                    },
                    new Note {
                        UserId = user.UserId,
                        Comments = "Matching criteria",
                        NoteDate = new System.DateTime(2019, 5, 5).ToUniversalTime()
                    }
                };

                context.Note.AddRange(notes);
                context.SaveChanges();

                var handler = new GetNotesRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetNotesRequest
                {
                    PageInfo = query,
                    FromDate = new System.DateTime(2018, 5, 5).ToUniversalTime()
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, 2);
                Assert.IsTrue(response.Result.Data.Items.All(x => x.Comments == "Matching criteria"));
            }
        }

        [Test]
        public void HandleAsync_ToDateIsSet_ReturnsNotesToTheDateRequested()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var notes = new List<Note>
                {
                    new Note {
                        UserId = user.UserId,
                        Comments = "Matching criteria",
                        NoteDate = new System.DateTime(2017, 5, 5).ToUniversalTime()
                    },
                    new Note {
                        UserId = user.UserId,
                        Comments = "Matching criteria",
                        NoteDate = new System.DateTime(2018, 5, 5).ToUniversalTime()
                    },
                    new Note {
                        UserId = user.UserId,
                        Comments = "Unmatching criteria",
                        NoteDate = new System.DateTime(2019, 5, 5).ToUniversalTime()
                    }
                };

                context.Note.AddRange(notes);
                context.SaveChanges();

                var handler = new GetNotesRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetNotesRequest
                {
                    PageInfo = query,
                    ToDate = new System.DateTime(2018, 5, 5).ToUniversalTime()
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, 2);
                Assert.IsTrue(response.Result.Data.Items.All(x => x.Comments == "Matching criteria"));
            }
        }

        [Test]
        public void HandleAsync_FromAndToDateIsSet_ReturnsNotesInTheDateRangeRequested()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var notes = new List<Note>
                {
                    new Note {
                        UserId = user.UserId,
                        Comments = "Unmatching criteria",
                        NoteDate = new System.DateTime(2017, 5, 4).ToUniversalTime()
                    },
                    new Note {
                        UserId = user.UserId,
                        Comments = "Matching criteria",
                        NoteDate = new System.DateTime(2017, 5, 5).ToUniversalTime()
                    },
                    new Note {
                        UserId = user.UserId,
                        Comments = "Matching criteria",
                        NoteDate = new System.DateTime(2018, 5, 5).ToUniversalTime()
                    },

                   new Note {
                        UserId = user.UserId,
                        Comments = "Matching criteria",
                        NoteDate = new System.DateTime(2018, 5, 5).ToUniversalTime()
                    },

                    new Note {
                        UserId = user.UserId,
                        Comments = "Unmatching criteria",
                        NoteDate = new System.DateTime(2018, 5, 6).ToUniversalTime()
                    }
                };

                context.Note.AddRange(notes);
                context.SaveChanges();

                var handler = new GetNotesRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetNotesRequest
                {
                    PageInfo = query,
                    FromDate = new System.DateTime(2017, 5, 5).ToUniversalTime(),
                    ToDate = new System.DateTime(2018, 5, 5).ToUniversalTime()
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, 3);
                Assert.IsTrue(response.Result.Data.Items.All(x => x.Comments == "Matching criteria"));
            }
        }

        [Test]
        public void HandleAsync_CommentsAreSet_ReturnsNotesWithRequestedComments()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var user = new User { UserId = 1 };
                context.User.Add(user);
                context.SaveChanges();

                var notes = new List<Note>
                {
                    new Note {
                        UserId = user.UserId,
                        Comments = "The match is inside of this comment - obviously :)",
                        NoteId = 1,
                    },
                    new Note {
                        UserId = user.UserId,
                        Comments = "matc - ooh, so close",
                        NoteId = 2,
                    },
                    new Note {
                        UserId = user.UserId,
                        Comments = "This comment does not contact the search query",
                        NoteId = 3
                    },
                };

                context.Note.AddRange(notes);
                context.SaveChanges();

                var handler = new GetNotesRequestHandler(
                    context,
                    GetUserContextStub(user.UserId));

                var query = new PagedQueryRequest { PageNumber = 1, PageSize = 5 };

                // Act
                var response = handler.HandleAsync(new GetNotesRequest
                {
                    PageInfo = query,
                    Comments = "match"
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
                Assert.IsNotNull(response.Result.Data);
                Assert.AreEqual(response.Result.Data.TotalItemCount, 1);
                Assert.AreEqual(1, response.Result.Data.Items[0].NoteId);
            }
        }
    }
}