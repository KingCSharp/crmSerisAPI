using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Geocoding;
using crmSeries.Core.Features.Mocks;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using NSubstitute;
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
        [Test]
        public void HandleAsync_NoIssues_LatLongAddedToBranchSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            var latitutde = "35.929673";
            var longitude = "-78.948237";

            var mediatorSubstitute =
                Substitute.For<IMediator>();

            mediatorSubstitute.HandleAsync(Arg.Any<GetGeocodeInfoRequest>())
                .Returns(new GeocodeInfoDto
                {
                    Results = new List<Result>
                    {
                        new Result
                        {
                            Location = new Location
                            {
                                Lat = latitutde,
                                Lng = longitude
                            }
                        }
                    }
                }.AsResponseAsync());

            mediatorSubstitute.HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                .Returns(new Response()).AsResponseAsync();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new AddGeocodeInfoHandler(context, mediatorSubstitute);

                context.Branch.Add(new Branch { BranchId = 1 });
                context.SaveChanges();

                // Act
                var response = handler.HandleAsync(new AddGeocodeInfoRequest
                {
                    RelatedRecordId = 1,
                    RelatedRecordType = "Branch",
                    Street = "Street",
                    City = "City",
                    State = "State",
                    PostalCode = "70808",
                    Country = "Country"
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var record = context.Branch.SingleOrDefault(x => x.BranchId == 1);
                Assert.IsNotNull(record);
                Assert.AreEqual(decimal.Parse(latitutde), record.Latitude);
                Assert.AreEqual(decimal.Parse(longitude), record.Longitude);
            }
        }

        [Test]
        public void HandleAsync_NoIssues_LatLongAddedToBrokerSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            var latitutde = "35.929673";
            var longitude = "-78.948237";

            var mediatorSubstitute =
                Substitute.For<IMediator>();

            mediatorSubstitute.HandleAsync(Arg.Any<GetGeocodeInfoRequest>())
                .Returns(new GeocodeInfoDto
                {
                    Results = new List<Result>
                    {
                        new Result
                        {
                            Location = new Location
                            {
                                Lat = latitutde,
                                Lng = longitude
                            }
                        }
                    }
                }.AsResponseAsync());

            mediatorSubstitute.HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                .Returns(new Response()).AsResponseAsync();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new AddGeocodeInfoHandler(context, mediatorSubstitute);

                context.Broker.Add(new Broker { BrokerId = 1 });
                context.SaveChanges();

                // Act
                var response = handler.HandleAsync(new AddGeocodeInfoRequest
                {
                    RelatedRecordId = 1,
                    RelatedRecordType = "Broker",
                    Street = "Street",
                    City = "City",
                    State = "State",
                    PostalCode = "70808",
                    Country = "Country"
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var record = context.Broker.SingleOrDefault(x => x.BrokerId == 1);
                Assert.IsNotNull(record);
                Assert.AreEqual(decimal.Parse(latitutde), record.Latitude);
                Assert.AreEqual(decimal.Parse(longitude), record.Longitude);
            }
        }

        [Test]
        public void HandleAsync_NoIssues_LatLongAddedToCompanySuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            var latitutde = "35.929673";
            var longitude = "-78.948237";

            var mediatorSubstitute =
                Substitute.For<IMediator>();

            mediatorSubstitute.HandleAsync(Arg.Any<GetGeocodeInfoRequest>())
                .Returns(new GeocodeInfoDto
                {
                    Results = new List<Result>
                    {
                        new Result
                        {
                            Location = new Location
                            {
                                Lat = latitutde,
                                Lng = longitude
                            }
                        }
                    }
                }.AsResponseAsync());

            mediatorSubstitute.HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                .Returns(new Response()).AsResponseAsync();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new AddGeocodeInfoHandler(context, mediatorSubstitute);

                context.Company.Add(new Company { CompanyId = 1 });
                context.SaveChanges();

                // Act
                var response = handler.HandleAsync(new AddGeocodeInfoRequest
                {
                    RelatedRecordId = 1,
                    RelatedRecordType = "Company",
                    Street = "Street",
                    City = "City",
                    State = "State",
                    PostalCode = "70808",
                    Country = "Country"
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var record = context.Company.SingleOrDefault(x => x.CompanyId == 1);
                Assert.IsNotNull(record);
                Assert.AreEqual(decimal.Parse(latitutde), record.Latitude);
                Assert.AreEqual(decimal.Parse(longitude), record.Longitude);
            }
        }

        [Test]
        public void HandleAsync_NoIssues_LatLongAddedToCompanyAssignedAddressSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            var latitutde = "35.929673";
            var longitude = "-78.948237";

            var mediatorSubstitute =
                Substitute.For<IMediator>();

            mediatorSubstitute.HandleAsync(Arg.Any<GetGeocodeInfoRequest>())
                .Returns(new GeocodeInfoDto
                {
                    Results = new List<Result>
                    {
                        new Result
                        {
                            Location = new Location
                            {
                                Lat = latitutde,
                                Lng = longitude
                            }
                        }
                    }
                }.AsResponseAsync());

            mediatorSubstitute.HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                .Returns(new Response()).AsResponseAsync();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new AddGeocodeInfoHandler(context, mediatorSubstitute);

                context.CompanyAssignedAddress.Add(new CompanyAssignedAddress { AddressId = 1 });
                context.SaveChanges();

                // Act
                var response = handler.HandleAsync(new AddGeocodeInfoRequest
                {
                    RelatedRecordId = 1,
                    RelatedRecordType = "CompanyAssignedAddress",
                    Street = "Street",
                    City = "City",
                    State = "State",
                    PostalCode = "70808",
                    Country = "Country"
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var record = context.CompanyAssignedAddress.SingleOrDefault(x => x.AddressId == 1);
                Assert.IsNotNull(record);
                Assert.AreEqual(decimal.Parse(latitutde), record.Latitude);
                Assert.AreEqual(decimal.Parse(longitude), record.Longitude);
            }
        }

        [Test]
        public void HandleAsync_NoIssues_LatLongAddedToContactAssignedAddressSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            var latitutde = "35.929673";
            var longitude = "-78.948237";

            var mediatorSubstitute =
                Substitute.For<IMediator>();

            mediatorSubstitute.HandleAsync(Arg.Any<GetGeocodeInfoRequest>())
                .Returns(new GeocodeInfoDto
                {
                    Results = new List<Result>
                    {
                        new Result
                        {
                            Location = new Location
                            {
                                Lat = latitutde,
                                Lng = longitude
                            }
                        }
                    }
                }.AsResponseAsync());

            mediatorSubstitute.HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                .Returns(new Response()).AsResponseAsync();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new AddGeocodeInfoHandler(context, mediatorSubstitute);

                context.ContactAssignedAddress.Add(new ContactAssignedAddress { AddressId = 1 });
                context.SaveChanges();

                // Act
                var response = handler.HandleAsync(new AddGeocodeInfoRequest
                {
                    RelatedRecordId = 1,
                    RelatedRecordType = "ContactAssignedAddress",
                    Street = "Street",
                    City = "City",
                    State = "State",
                    PostalCode = "70808",
                    Country = "Country"
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var record = context.ContactAssignedAddress.SingleOrDefault(x => x.AddressId == 1);
                Assert.IsNotNull(record);
                Assert.AreEqual(decimal.Parse(latitutde), record.Latitude);
                Assert.AreEqual(decimal.Parse(longitude), record.Longitude);
            }
        }

        [Test]
        public void HandleAsync_NoIssues_LatLongAddedToEquipmentSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            var latitutde = "35.929673";
            var longitude = "-78.948237";

            var mediatorSubstitute =
                Substitute.For<IMediator>();

            mediatorSubstitute.HandleAsync(Arg.Any<GetGeocodeInfoRequest>())
                .Returns(new GeocodeInfoDto
                {
                    Results = new List<Result>
                    {
                        new Result
                        {
                            Location = new Location
                            {
                                Lat = latitutde,
                                Lng = longitude
                            }
                        }
                    }
                }.AsResponseAsync());

            mediatorSubstitute.HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                .Returns(new Response()).AsResponseAsync();

            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new AddGeocodeInfoHandler(context, mediatorSubstitute);

                context.Equipment.Add(new Domain.HeavyEquipment.Equipment { EquipmentId = 1 });
                context.SaveChanges();

                // Act
                var response = handler.HandleAsync(new AddGeocodeInfoRequest
                {
                    RelatedRecordId = 1,
                    RelatedRecordType = "Equipment",
                    Street = "Street",
                    City = "City",
                    State = "State",
                    PostalCode = "70808",
                    Country = "Country"
                });

                //Assert 
                Assert.AreEqual(false, response.Result.HasErrors);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var record = context.Equipment.SingleOrDefault(x => x.EquipmentId == 1);
                Assert.IsNotNull(record);
                Assert.AreEqual(decimal.Parse(latitutde), record.Latitude);
                Assert.AreEqual(decimal.Parse(longitude), record.Longitude);
            }
        }

        [Test]
        public void HandleAsync_NoIssues_LatLongAddedToLeadSuccessfully()
        {
            // Arrange 
            var options = GetHeavyEquipmentContextOptions();

            var latitutde = "35.929673";
            var longitude = "-78.948237";

            var mediatorSubstitute =
                Substitute.For<IMediator>();

            mediatorSubstitute.HandleAsync(Arg.Any<GetGeocodeInfoRequest>())
                .Returns(new GeocodeInfoDto {
                    Results = new List<Result>
                    {
                        new Result
                        {
                            Location = new Location
                            {
                                Lat = latitutde,
                                Lng = longitude
                            }
                        }
                    }
                }.AsResponseAsync());

            mediatorSubstitute.HandleAsync(Arg.Any<VerifyRelatedRecordRequest>())
                .Returns(new Response()).AsResponseAsync();
             
            using (var context = new HeavyEquipmentContext(options))
            {
                var handler = new AddGeocodeInfoHandler(context, mediatorSubstitute);

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
                Assert.AreEqual(false, response.Result.HasErrors);
            }

            using (var context = new HeavyEquipmentContext(options))
            {
                var record = context.Lead.SingleOrDefault(x => x.LeadId == 1);
                Assert.IsNotNull(record);
                Assert.AreEqual(decimal.Parse(latitutde), record.Latitude);
                Assert.AreEqual(decimal.Parse(longitude), record.Longitude);
            }
        }
    }
}
