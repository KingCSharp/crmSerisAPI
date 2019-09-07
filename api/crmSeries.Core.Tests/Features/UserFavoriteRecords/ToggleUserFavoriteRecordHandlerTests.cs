using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.UserFavoriteRecords;
using crmSeries.Core.Mediator;
using crmSeries.Core.Security;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace crmSeries.Core.Tests.Features.UserFavoriteRecords
{
    [TestFixture]
    public class ToggleUserFavoriteRecordHandlerTests : BaseUnitTest
    {
        [Test]
        public void HandleAsync_NoIssuesUserIdPassed_RecordGetsAddedAsFavoriteForUserPassedIn()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            var userId = 12;

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = Substitute.For<IRequestHandler<VerifyRelatedRecordRequest>>();
                verificationHandler
                    .HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                    .Returns(new Response());

                var addFavoriteRecordHandler = new AddUserFavoriteRecordHandler(context, verificationHandler);

                var identityContext = Substitute.For<IIdentityUserContext>();
                identityContext.RequestingUser.Returns(x => new IdentityUser
                {
                    UserId = 35
                });

                var handler = new ToggleUserFavoriteRecordHandler(
                    context,
                    verificationHandler,
                    addFavoriteRecordHandler,
                    null,
                    identityContext);

                context.User.Add(new User
                {
                    UserId = userId
                });
                context.SaveChanges();

                context.Contact.Add(new Contact
                {
                    ContactId = 2
                });
                context.SaveChanges();

                // Act
                var response = handler.HandleAsync(new ToggleUserFavoriteRecordRequest
                {
                    RecordId = 2,
                    RecordType = Constants.RelatedRecord.Types.Contact,
                    UserId = userId
                });

                Assert.AreEqual(false, response.Result.HasErrors);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var favoritedEntity = context.UserFavoriteRecord.Single(
                    x => x.RecordId == 2);

                Assert.IsNotNull(favoritedEntity);
                Assert.AreEqual(2, favoritedEntity.RecordId);
                Assert.AreEqual(userId, favoritedEntity.UserId);
                Assert.AreEqual(Constants.RelatedRecord.Types.Contact, favoritedEntity.RecordType);
            }
        }

        [Test]
        public void HandleAsync_NoIssuesUserIdNotPassed_RecordGetsAddedAsFavoriteForRequestingUser()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            var userId = 33;

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = Substitute.For<IRequestHandler<VerifyRelatedRecordRequest>>();
                verificationHandler
                    .HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                    .Returns(new Response());

                var addFavoriteRecordHandler = new AddUserFavoriteRecordHandler(context, verificationHandler);
                var deleteFavoriteRecordHandler = new DeleteUserFavoriteRecordHandler(context);

                var identityContext = Substitute.For<IIdentityUserContext>();
                identityContext.RequestingUser.Returns(x => new IdentityUser
                {
                    UserId = userId
                });

                var handler = new ToggleUserFavoriteRecordHandler(
                    context, 
                    verificationHandler, 
                    addFavoriteRecordHandler, 
                    deleteFavoriteRecordHandler,
                    identityContext);

                context.User.Add(new User
                {
                    UserId = userId
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
                    UserId = 0
                });

                var favoritedEntity = context.UserFavoriteRecord.Single(
                    x => x.RecordId == response.Id);
                                
                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var favoriteRecord = context.UserFavoriteRecord.SingleOrDefault(x => x.FavoriteId == 1);
                Assert.IsNotNull(favoriteRecord);
                Assert.AreEqual(1, favoriteRecord.RecordId);
                Assert.AreEqual(userId, favoriteRecord.UserId);
                Assert.AreEqual(Constants.RelatedRecord.Types.Company, favoriteRecord.RecordType);
            }
        }

        [Test]
        public void HandleAsync_NoIssuesUnFavoriteUserIdPassed_RecordGetsRemovedFromFavoriteForUserPassedIn()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = Substitute.For<IRequestHandler<VerifyRelatedRecordRequest>>();
                verificationHandler
                    .HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                    .Returns(new Response());

                var addFavoriteRecordHandler = new AddUserFavoriteRecordHandler(context, verificationHandler);
                var deleteFavoriteRecordHandler = new DeleteUserFavoriteRecordHandler(context);

                var identityContext = Substitute.For<IIdentityUserContext>();
                identityContext.RequestingUser.Returns(x => new IdentityUser
                {
                    UserId = 33
                });

                var handler = new ToggleUserFavoriteRecordHandler(
                    context,
                    verificationHandler,
                    addFavoriteRecordHandler,
                    deleteFavoriteRecordHandler,
                    identityContext);

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
                var favoriteRecord = context.UserFavoriteRecord.SingleOrDefault(x => x.FavoriteId == 1);
                Assert.IsNull(favoriteRecord);
            }
        }

        [Test]
        public void HandleAsync_NoIssuesUnFavoriteUserIdPassed_RecordGetsRemovedFromFavoriteForRequestingUser()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var verificationHandler = Substitute.For<IRequestHandler<VerifyRelatedRecordRequest>>();
                verificationHandler
                    .HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                    .Returns(new Response());

                var addFavoriteRecordHandler = new AddUserFavoriteRecordHandler(context, verificationHandler);
                var deleteFavoriteRecordHandler = new DeleteUserFavoriteRecordHandler(context);

                var userId = 33;
                var identityContext = Substitute.For<IIdentityUserContext>();
                identityContext.RequestingUser.Returns(x => new IdentityUser
                {
                    UserId = userId
                });

                var handler = new ToggleUserFavoriteRecordHandler(
                    context,
                    verificationHandler,
                    addFavoriteRecordHandler,
                    deleteFavoriteRecordHandler,
                    identityContext);

                context.User.Add(new User
                {
                    UserId = userId
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
                    UserId = userId
                });

                context.SaveChanges();

                // Act
                var response = handler.HandleAsync(new ToggleUserFavoriteRecordRequest
                {
                    RecordId = 1,
                    RecordType = Constants.RelatedRecord.Types.Company,
                    UserId = 0
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
                var favoriteRecord = context.UserFavoriteRecord.SingleOrDefault(x => x.FavoriteId == 1);
                Assert.IsNull(favoriteRecord);
            }
        }
    }
}
