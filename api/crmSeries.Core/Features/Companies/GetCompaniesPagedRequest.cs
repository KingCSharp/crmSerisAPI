using crmSeries.Core.Data;
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
using crmSeries.Core.Common;
using AutoMapper.QueryableExtensions;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompaniesPagedRequest : IRequest<PagedQueryResult<GetCompanyDto>>
    {
        public PagedQueryRequest Query { get; set; }
    }

    public class GetCompaniesPagedRequestHandler : IRequestHandler<GetCompaniesPagedRequest, PagedQueryResult<GetCompanyDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;
        public GetCompaniesPagedRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<GetCompanyDto>>> HandleAsync(GetCompaniesPagedRequest request)
        {
            var result = new PagedQueryResult<GetCompanyDto>();

            var companyTotalList = (from companies in _context.Company
                               join assignedUser in _context.CompanyAssignedUser
                                on companies.CompanyId equals assignedUser.CompanyId
                               where assignedUser.UserId == _identity.RequestingUser.CurrentUser.UserId
                               && !companies.Deleted
                               select companies)
                .ProjectTo<GetCompanyDto>()
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

            result.Items = companyList;
            result.Items
                .Where(x => favorites.Contains(x.CompanyId))
                .ToList()
                .ForEach(x =>
                {
                    x.Favorite = true;
                });

            result.PageCount = resultCount / request.Query.PageSize;
            result.TotalItemCount = resultCount;
            result.PageNumber = request.Query.PageNumber;
            result.PageSize = request.Query.PageSize;

            return result.AsResponseAsync();
        }
    }

    public class GetCompaniesPagedValidator : AbstractValidator<GetCompaniesPagedRequest>
    {
        public GetCompaniesPagedValidator()
        {
            RuleFor(x => x.Query.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.Query.PageSize)
                .GreaterThan(0);
        }
    }
}
