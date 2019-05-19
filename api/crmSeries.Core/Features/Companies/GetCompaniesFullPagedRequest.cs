using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using crmSeries.Core.Features.CompanyAssignedAddresses.Dtos;
using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Common;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompaniesFullPagedRequest : IRequest<PagedQueryResult<CompanyFullDto>>
    {
        public PagedQueryRequest Query { get; set; }
    }

    public class GetCompaniesFullPagedRequestHandler : 
        IRequestHandler<GetCompaniesFullPagedRequest, PagedQueryResult<CompanyFullDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;
        public GetCompaniesFullPagedRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<CompanyFullDto>>> HandleAsync(GetCompaniesFullPagedRequest request)
        {
            var resultList = new List<CompanyFullDto>();
            var result = new PagedQueryResult<CompanyFullDto>();

            if (_identity.RequestingUser.CurrentUser != null)
            {
                var companyTotalList = (from companies in _context.Company
                                   join assignedUser in _context.CompanyAssignedUser
                                    on companies.CompanyId equals assignedUser.CompanyId
                                   where assignedUser.UserId == _identity.RequestingUser.CurrentUser.UserId
                                   select companies)
                    .OrderBy(x => x.CompanyId)
                    .Distinct();

                int resultCount = companyTotalList.Count();

                var companyList = companyTotalList
                    .Skip((request.Query.PageNumber - 1) * request.Query.PageSize)
                    .Take(request.Query.PageSize)
                    .ToList();

                var favorites = _context.UserFavoriteRecord
                .Where(x =>
                    x.RecordType == Constants.UserFavoriteRecords.Types.Company &&
                    x.UserId == _identity.RequestingUser.CurrentUser.UserId)
                .Select(x => x.RecordId)
                .ToList();

                foreach (var company in companyList)
                {
                    var companyDto = new CompanyFullDto
                    {
                        Details = Mapper.Map<CompanyDto>(company),
                        Addresses = Mapper.Map<List<CompanyAssignedAddressDto>>(
                            _context.CompanyAssignedAddress
                            .Where(x => x.CompanyId == company.CompanyId)
                            .ToList()),
                        Contacts = Mapper.Map<List<GetContactDto>>(
                            _context.Contact
                            .Where(x => x.CompanyId == company.CompanyId)
                            .ToList())
                    };
                    companyDto.Details.Favorite = favorites.Contains(company.CompanyId);
                    resultList.Add(companyDto);
                }

                result.Items = resultList;
                result.PageCount = resultCount / request.Query.PageSize;
                result.TotalItemCount = resultCount;
                result.PageNumber = request.Query.PageNumber;
                result.PageSize = request.Query.PageSize;
            }

            return result.AsResponseAsync();
        }
    }

    public class GetCompaniesFullPagedValidator : AbstractValidator<GetCompaniesFullPagedRequest>
    {
        public GetCompaniesFullPagedValidator()
        {
            RuleFor(x => x.Query.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.Query.PageSize)
                .GreaterThan(0);
        }
    }
}
