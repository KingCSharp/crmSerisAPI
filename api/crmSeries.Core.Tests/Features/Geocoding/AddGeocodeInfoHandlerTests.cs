using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Geocoding;
using crmSeries.Core.Features.Mocks;
using crmSeries.Core.Mediator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crmSeries.Core.Tests.Features.Geocoding
{
    [TestFixture]
    public class AddGeocodeInfoHandlerTests : BaseUnitTest
    {
        [SetUp]
        public void SetUpMocks()
        {
            Container.Options.AllowOverridingRegistrations = true;
//            Container.RegisterInstance<IRequestHandler<GetGeocodeInfoRequest, GeocodeInfoDto>>(new MockGetGeocodeInfoHandler());
            Container.Options.AllowOverridingRegistrations = true;
        }

        //[Test]
        public void HandleAsync_NoIssues_LatLongAddedToLeadSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new AddGeocodeInfoHandler(context, Mediator);

                context.Lead.Add(new Lead { LeadId = 1 });
                context.SaveChanges();

                // Act
                var response = handler.HandleAsync(new AddGeocodeInfoRequest
                {
                    RelatedRecordId = 1,
                    RelatedRecordType = "Lead",
                    Street = "Street",
                    City = "City",
                    State = "State",
                    PostalCode = "70808",
                    Country = "Country"
                });

                //Assert 
                Assert.AreEqual(response.Result.HasErrors, false);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var record = context.Lead.SingleOrDefault(x => x.LeadId == 1);
                Assert.IsNotNull(record);
                Assert.AreEqual(record.Latitude, 35.929673);
                Assert.AreEqual(record.Longitude, -78.948237);
            }
        }
    }
}
