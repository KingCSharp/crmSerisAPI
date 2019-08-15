using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Features.Contacts.Validator;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.CompanyAssignedCategories;
using System.Linq;
using crmSeries.Core.Features.CompanyAssignedRanks.Dtos;
using crmSeries.Core.Features.CompanyAssignedRanks;
using crmSeries.Core.Features.Geocoding;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class AddCompanyRequest : AddCompanyDto, IRequest<AddResponse>
    {
    }

    public class AddCompanyHandler : IRequestHandler<AddCompanyRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IMediator _mediator;
        private readonly IRequestHandler<AddCompanyAssignedCategoryRequest, AddResponse> _addCompanyAssignedCategoryHandler;
        private readonly IRequestHandler<AddCompanyAssignedRankRequest, AddResponse> _addCompanyAssignedRankHandler;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public AddCompanyHandler(HeavyEquipmentContext context,
            IMediator mediator,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler,
            IRequestHandler<AddCompanyAssignedCategoryRequest, AddResponse> addCompanyAssignedCategoryHandler,
            IRequestHandler<AddCompanyAssignedRankRequest, AddResponse> addCompanyAssignedRankHandler)
        {
            _context = context;
            _mediator = mediator;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
            _addCompanyAssignedCategoryHandler = addCompanyAssignedCategoryHandler;
            _addCompanyAssignedRankHandler = addCompanyAssignedRankHandler;
        }

        public Task<Response<AddResponse>> HandleAsync(AddCompanyRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var company = request.MapTo<Company>();
            company.LastModified = DateTime.UtcNow;

            AssignGeocodeInfo(company);

            _context.Set<Company>().Add(company);
            _context.SaveChanges();

            AddAssignedCategories(request.Categories, company.CompanyId);
            AddAssignedRanks(request.Ranks, company.CompanyId);

            return new AddResponse
            {
                Id = company.CompanyId
            }.AsResponseAsync();
        }

        private bool IsValid(AddCompanyRequest request, out Task<Response<AddResponse>> errorAsync)
        {
            var relatedEntities = new List<(string, int)>();

            request.Categories?.ForEach(x => 
            {
                relatedEntities.Add((Constants.RelatedRecord.Types.CompanyCategory, x));
            });

            request.Ranks?.ForEach(x =>
            {
                relatedEntities.Add((Constants.RelatedRecord.Types.Rank, x.RankId));
                relatedEntities.Add((Constants.RelatedRecord.Types.UserRole, x.RoleId));
            });


            foreach (var (relatedRecordType, relatedRecordTypeId) in relatedEntities)
            {
                var verifyRelatedRecordRequest = new VerifyRelatedRecordRequest
                {
                    RecordType = relatedRecordType,
                    RecordTypeId = relatedRecordTypeId
                };

                var result = _verifyRelatedRecordsHandler.HandleAsync(verifyRelatedRecordRequest).Result;

                if (result.HasErrors)
                {
                    errorAsync = Response<AddResponse>.ErrorsAsync(result.Errors);
                    return false;
                }
            }

            errorAsync = null;
            return true;
        }

        private void AssignGeocodeInfo(Company company)
        {
            var geocodingRequest = new GetGeocodeInfoRequest
            {
                Street = company.Address1,
                City = company.City,
                State = company.State,
                PostalCode = company.Zip,
                Country = company.Country
            };

            var result = _mediator.HandleAsync(geocodingRequest).Result;

            if (!result.HasErrors && result?.Data?.Results != null)
            {
                var geocodeInfo = result.Data
                    .Results
                    .OrderByDescending(x => x.Accuracy)
                    .FirstOrDefault();

                if (geocodeInfo?.Location != null)
                {
                    company.Latitude = decimal.Parse(geocodeInfo.Location.Lat);
                    company.Longitude = decimal.Parse(geocodeInfo.Location.Lng);
                }
            }
        }

        private void AddAssignedCategories(List<int> categories, int companyId)
        {
            if (categories == null)
                return;

            categories = categories.Distinct().ToList();
            categories.RemoveAll(x => x == 0);

            categories.ForEach(x =>
            {
                _addCompanyAssignedCategoryHandler.HandleAsync(new AddCompanyAssignedCategoryRequest
                {
                    CategoryId = x,
                    CompanyId = companyId
                });
            });
        }

        private void AddAssignedRanks(List<BaseAddCompanyAssignedRankDto> ranks, int companyId)
        {
            if (ranks == null)
                return;

            ranks = ranks.Distinct().ToList();
            ranks.RemoveAll(x => x.RankId == 0 || x.RoleId == 0);

            ranks.ForEach(x =>
            {
                _addCompanyAssignedRankHandler.HandleAsync(new AddCompanyAssignedRankRequest
                {
                    CompanyId = companyId,
                    RankId = x.RankId,
                    RoleId = x.RoleId
                });
            });
        }
    }

    public class AddCompanyValidator : AbstractValidator<AddCompanyRequest>
    {
        public AddCompanyValidator()
        {
            Include(new BaseCompanyDtoValidator());
        }
    }
}