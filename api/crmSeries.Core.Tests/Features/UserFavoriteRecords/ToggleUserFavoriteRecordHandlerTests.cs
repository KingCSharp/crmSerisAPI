using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.UserFavoriteRecords;
using crmSeries.Core.Features.Users.Utility;
using NUnit.Framework;
using System;
using System.Linq;

namespace crmSeries.Core.Tests.Features.UserFavoriteRecords
{
    [TestFixture]
    public class ToggleUserFavoriteRecordHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssuesFavorite_RecordGetsAddedAsFavoriteForUser()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var addFavoriteRecordHandler = new AddUserFavoriteRecordHandler(context, verificationHandler);
                var deleteFavoriteRecordHandler = new DeleteUserFavoriteRecordHandler(context);
                var handler = new ToggleUserFavoriteRecordHandler(
                    context, verificationHandler, addFavoriteRecordHandler, deleteFavoriteRecordHandler);

                context.User.Add(new User
                {
                    UserId = 1
                });

                context.Company.Add(new Company
                {
                    CompanyId = 1
                });

                context.SaveChanges();

                // Act
                var response = handler.HandleAsync(new ToggleUserFavoriteRecordRequest
                {
                    RecordId = 1,
                    RecordType = Constants.RelatedRecord.Types.Company,
                    UserId = 1
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var favoriteRecord = context.UserFavoriteRecord.SingleOrDefault(x => x.FavoriteId == 1);
                Assert.IsNotNull(favoriteRecord);
                Assert.AreEqual(1, favoriteRecord.RecordId);
                Assert.AreEqual(1, favoriteRecord.UserId);
                Assert.AreEqual(Constants.RelatedRecord.Types.Company, favoriteRecord.RecordType);
            }
        }

        [Test]
        public void HandleAsync_NoIssuesUnFavorite_RecordGetsRemovedFromFavoriteForUser()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = new VerifyRelatedRecordHandler(context);
                var addFavoriteRecordHandler = new AddUserFavoriteRecordHandler(context, verificationHandler);
                var deleteFavoriteRecordHandler = new DeleteUserFavoriteRecordHandler(context);
                var handler = new ToggleUserFavoriteRecordHandler(
                    context, verificationHandler, addFavoriteRecordHandler, deleteFavoriteRecordHandler);

                context.User.Add(new User
                {
                    UserId = 1
                });

                context.Company.Add(new Company
                {
                    CompanyId = 1
                });

                context.UserFavoriteRecord.Add(new UserFavoriteRecord
                {
                    FavoriteId = 1,
                    RecordId = 1,
                    RecordType = Constants.RelatedRecord.Types.Company,
                    UserId = 1
                });

                context.SaveChanges();

                // Act
                var response = handler.HandleAsync(new ToggleUserFavoriteRecordRequest
                {
                    RecordId = 1,
                    RecordType = Constants.RelatedRecord.Types.Company,
                    UserId = 1
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var favoriteRecord = context.UserFavoriteRecord.SingleOrDefault(x => x.FavoriteId == 1);
                Assert.IsNull(favoriteRecord);
            }
        }        
    }
}
