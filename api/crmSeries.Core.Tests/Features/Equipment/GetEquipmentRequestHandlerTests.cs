using System.Linq;
using crmSeries.Core.Data;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Security;
using crmSeries.Core.Tests.Extensions;
using FizzWare.NBuilder;
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;

namespace crmSeries.Core.Tests.Features.Equipment
{
    [TestFixture]
    public class GetEquipmentRequestHandlerTests : BaseUnitTest
    {
        private HeavyEquipmentContext Context { get; set; }

        private IIdentityUserContext UserContext { get; set; }

        [SetUp]
        public void SetUp()
        {
            Context = new HeavyEquipmentContext(GetHeavyEquipmentContextOptions());
            UserContext = new StubbedIdentityUserContext(1);

            CreateBuilderPersistMethods<Domain.HeavyEquipment.Equipment>(Context);

            BuilderSetup.DisablePropertyNamingFor<Domain.HeavyEquipment.Equipment, int>(x => x.EquipmentId);
        }

        [TearDown]
        public void TearDown()
        {
            Context.Dispose();
        }

        [Test]
        public async Task HandleAsync_ValidRequest_ReturnsPagedResults()
        {
            Builder<Domain.HeavyEquipment.Equipment>.CreateListOfSize(30)
                .All()
                .With(x => x.Deleted = false)
                .With(x => x.Active = true)
                .With(x => x.Machine = true)
                .Persist();

            var handler = CreateHandler();
            var request = CreateRequest();

            var results = await handler.HandleAsync(request);

            results.AssertIsValid();
            Assert.That(results.Data.PageCount, Is.EqualTo(3));
            Assert.That(results.Data.Items, Has.Count.EqualTo(10));
        }

        [Test]
        public async Task HandleAsync_ValidRequest_FiltersOnlyActiveRecords()
        {
            Builder<Domain.HeavyEquipment.Equipment>.CreateListOfSize(10)
                .All()
                .With(x => x.Deleted = false)
                .With(x => x.Active = true)
                .With(x => x.Machine = true)
                .TheLast(5)
                .With(x => x.Active = false)
                .Persist();

            var handler = CreateHandler();
            var request = CreateRequest();

            var results = await handler.HandleAsync(request);

            results.AssertIsValid();
            Assert.That(results.Data.Items, Has.Count.EqualTo(5));
        }

        [Test]
        public async Task HandleAsync_ValidRequest_FiltersOutDeletedRecords()
        {
            Builder<Domain.HeavyEquipment.Equipment>.CreateListOfSize(10)
                .All()
                .With(x => x.Deleted = false)
                .With(x => x.Active = true)
                .With(x => x.Machine = true)
                .TheLast(5)
                .With(x => x.Deleted = true)
                .Persist();

            var handler = CreateHandler();
            var request = CreateRequest();

            var results = await handler.HandleAsync(request);

            results.AssertIsValid();
            Assert.That(results.Data.Items, Has.Count.EqualTo(5));
        }

        [TestCase(true, 7)]
        [TestCase(false, 3)]
        public async Task HandleAsync_ValidRequest_FiltersByIsMachine(bool isMachine, int expectedCount)
        {
            Builder<Domain.HeavyEquipment.Equipment>.CreateListOfSize(10)
                .All()
                .With(x => x.Deleted = false)
                .With(x => x.Active = true)
                .With(x => x.Machine = true)
                .TheLast(3)
                .With(x => x.Machine = false)
                .Persist();

            var handler = CreateHandler();
            var request = CreateRequest();

            request.IsMachine = isMachine;

            var results = await handler.HandleAsync(request);

            results.AssertIsValid();
            Assert.That(results.Data.Items, Has.Count.EqualTo(expectedCount));
        }

        [Test]
        public async Task HandleAsync_CategoryIdIsNotDefault_FiltersByCategoryId()
        {
            Builder<Domain.HeavyEquipment.Equipment>.CreateListOfSize(10)
                .All()
                .With(x => x.Deleted = false)
                .With(x => x.Active = true)
                .With(x => x.Machine = true)
                .With(x => x.CategoryId = 100)
                .TheLast(3)
                .With(x => x.CategoryId = 1)
                .Persist();

            var handler = CreateHandler();
            var request = CreateRequest();

            request.CategoryId = 1;

            var results = await handler.HandleAsync(request);

            results.AssertIsValid();
            Assert.That(results.Data.Items, Has.Count.EqualTo(3));
            Assert.That(results.Data.Items.Select(x => x.CategoryId), Has.All.EqualTo(1));
        }

        [Test]
        public async Task HandleAsync_ParentTypeNotNullOrEmpty_FiltersByParentType()
        {
            Builder<Domain.HeavyEquipment.Equipment>.CreateListOfSize(10)
                .All()
                .With(x => x.Deleted = false)
                .With(x => x.Active = true)
                .With(x => x.Machine = true)
                .With(x => x.ParentType = "ParentType")
                .TheLast(3)
                .With(x => x.ParentType = "Company")
                .Persist();

            var handler = CreateHandler();
            var request = CreateRequest();

            request.ParentType = "Company";

            var results = await handler.HandleAsync(request);

            results.AssertIsValid();
            Assert.That(results.Data.Items, Has.Count.EqualTo(3));
            Assert.That(results.Data.Items.Select(x => x.ParentType), Has.All.EqualTo("Company"));
        }

        [Test]
        public async Task HandleAsync_StartHoursMinAndStartHoursMaxNotDefault_FiltersBetweenStartHoursMinAndStartHoursMax()
        {
            var startHours = 3;

            Builder<Domain.HeavyEquipment.Equipment>.CreateListOfSize(10)
                .All()
                .With(x => x.Deleted = false)
                .With(x => x.Active = true)
                .With(x => x.Machine = true)
                .With(x => x.StartHours = 1)
                .TheLast(4)
                .With(x => x.StartHours = startHours++)
                .Persist();

            var handler = CreateHandler();
            var request = CreateRequest();

            request.StartHoursMin = 3;
            request.StartHoursMax = 6;

            var results = await handler.HandleAsync(request);

            results.AssertIsValid();
            Assert.That(results.Data.Items, Has.Count.EqualTo(4));
            Assert.That(results.Data.Items.Select(x => x.StartHours), Has.All.GreaterThanOrEqualTo(3));
            Assert.That(results.Data.Items.Select(x => x.StartHours), Has.All.LessThanOrEqualTo(6));
        }

        private GetEquipmentRequestHandler CreateHandler() => new GetEquipmentRequestHandler(Context, UserContext);

        private static GetEquipmentRequest CreateRequest() =>
            new GetEquipmentRequest
            {
                PageSize = 10,
                PageNumber = 1
            };
    }
}