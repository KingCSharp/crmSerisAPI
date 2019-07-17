using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Equipment;
using crmSeries.Core.Tests.Extensions;
using FizzWare.NBuilder;
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;

namespace crmSeries.Core.Tests.Features.Equipment
{
    [TestFixture]
    public class GetEquipmentCategoriesHandlerTests : BaseUnitTest
    {
        private HeavyEquipmentContext Context { get; set; }

        [SetUp]
        public void SetUp()
        {
            Context = new HeavyEquipmentContext(GetHeavyEquipmentContextOptions());

            CreateBuilderPersistMethods<EquipmentCategory>(Context);
        }

        [TearDown]
        public void TearDown()
        {
            Context.Dispose();
        }

        [Test]
        public async Task HandleAsync_ValidRequest_FiltersOutDeletedRecords()
        {
            Builder<EquipmentCategory>.CreateListOfSize(40)
                .All().With(x => x.Deleted = false)
                .TheLast(10).With(x => x.Deleted = true)
                .Persist();

            var handler = CreateHandler();
            var request = CreateRequest();

            var results = await handler.HandleAsync(request);

            results.AssertIsValid();
            Assert.That(results.Data.TotalItemCount, Is.EqualTo(30));
        }

        [Test]
        public async Task HandleAsync_ValidRequest_ReturnsPagedResults()
        {
            Builder<EquipmentCategory>.CreateListOfSize(30)
                .All().With(x => x.Deleted = false)
                .Persist();

            var handler = CreateHandler();
            var request = CreateRequest();

            var results = await handler.HandleAsync(request);

            results.AssertIsValid();
            Assert.That(results.Data.PageCount, Is.EqualTo(2));
            Assert.That(results.Data.Items, Has.Count.EqualTo(20));
        }

        private GetEquipmentCategoriesHandler CreateHandler() => new GetEquipmentCategoriesHandler(Context);

        private static GetEquipmentCategoriesRequest CreateRequest() =>
            new GetEquipmentCategoriesRequest
            {
                PageNumber = 1,
                PageSize = 20
            };
    }
}