using crmSeries.Core.Configuration;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Geocoding
{
    [DoNotValidate]
    public class AddGeocodeInfoRequest : IRequest
    {
        public int RelatedRecordId { get; set; }
        
        public string RelatedRecordType { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }

    public class AddGeocodeInfoHandler : IRequestHandler<AddGeocodeInfoRequest>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IMediator _mediator;

        public AddGeocodeInfoHandler(HeavyEquipmentContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public Task<Response> HandleAsync(AddGeocodeInfoRequest request)
        {
            var verificationResult = _mediator.HandleAsync(new VerifyRelatedRecordRequest
            {
                RecordTypeId = request.RelatedRecordId,
                RecordType = request.RelatedRecordType

            }).Result;

            if (verificationResult.HasErrors)
                return Response.ErrorsAsync(verificationResult.Errors);

            var geoCodeResult = _mediator.HandleAsync(new GetGeocodeInfoRequest
            {
                Street = request.Street,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode,
                Country = request.Country
            }).Result;

            if (geoCodeResult.HasErrors)
                return Response.ErrorsAsync(geoCodeResult.Errors);

            var geoCodeInfo = geoCodeResult.Data
                    .Results
                    .OrderByDescending(x => x.Accuracy)
                    .FirstOrDefault();

            var latitude = decimal.Parse(geoCodeInfo.Location.Lat);
            var longitude = decimal.Parse(geoCodeInfo.Location.Lng);

            switch (request.RelatedRecordType)
            {
                case Constants.RelatedRecord.Types.Branch:
                    AddGeoCodeInfoToBranch(request.RelatedRecordId, latitude, longitude);
                    break;

                case Constants.RelatedRecord.Types.Broker:
                    AddGeoCodeInfoToBroker(request.RelatedRecordId, latitude, longitude);
                    break;

                case Constants.RelatedRecord.Types.Company:
                    AddGeoCodeInfoToCompany(request.RelatedRecordId, latitude, longitude);
                    break;

                case Constants.RelatedRecord.Types.CompanyAssignedAddress:
                    AddGeoCodeInfoToCompanyAssignedAddress(request.RelatedRecordId, latitude, longitude);
                    break;

                case Constants.RelatedRecord.Types.ContactAssignedAddress:
                    AddGeoCodeInfoToContactAssignedAddress(request.RelatedRecordId, latitude, longitude);
                    break;

                case Constants.RelatedRecord.Types.Equipment:
                    AddGeoCodeInfoToEquipment(request.RelatedRecordId, latitude, longitude);
                    break;

                case Constants.RelatedRecord.Types.Lead:
                    AddGeoCodeInfoToLead(request.RelatedRecordId, latitude, longitude);
                    break;
                default:
                    return Response.ErrorAsync($"Unsupported record type {request.RelatedRecordType}");
            }

            return Response.SuccessAsync();
        }

        private void AddGeoCodeInfoToLead(int entityId, decimal latitude, decimal longitude)
        {
            var lead = _context.Set<Lead>().Find(entityId);
            lead.Latitude = latitude;
            lead.Longitude = longitude;

            _context.SaveChanges();
        }

        private void AddGeoCodeInfoToEquipment(int entityId, decimal latitude, decimal longitude)
        {
            var equipment = _context.Set<Domain.HeavyEquipment.Equipment>().Find(entityId);
            equipment.Latitude = latitude;
            equipment.Longitude = longitude;
            _context.SaveChanges();
        }

        private void AddGeoCodeInfoToContactAssignedAddress(int entityId, decimal latitude, decimal longitude)
        {
            var contactAssignedAddress = _context.Set<ContactAssignedAddress>().Find(entityId);
            contactAssignedAddress.Latitude = latitude;
            contactAssignedAddress.Longitude = longitude;
            _context.SaveChanges();
        }

        private void AddGeoCodeInfoToCompanyAssignedAddress(int entityId, decimal latitude, decimal longitude)
        {
            var companyAssignedAddress = _context.Set<CompanyAssignedAddress>().Find(entityId);
            companyAssignedAddress.Latitude = latitude;
            companyAssignedAddress.Longitude = longitude;
            _context.SaveChanges();
        }

        private void AddGeoCodeInfoToCompany(int entityId, decimal latitude, decimal longitude)
        {
            var company = _context.Set<Company>().Find(entityId);
            company.Latitude = latitude;
            company.Longitude = longitude;
            _context.SaveChanges();
        }

        private void AddGeoCodeInfoToBroker(int entityId, decimal latitude, decimal longitude)
        {
            var broker = _context.Set<Broker>().Find(entityId);
            broker.Latitude = latitude;
            broker.Longitude = longitude;
            _context.SaveChanges();
        }

        private void AddGeoCodeInfoToBranch(int entityId, decimal latitude, decimal longitude)
        {
            var branch = _context.Set<Branch>().Find(entityId);
            branch.Latitude = latitude;
            branch.Longitude = longitude;
            _context.SaveChanges();
        }
    }
}
