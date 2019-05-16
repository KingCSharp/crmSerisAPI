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

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompaniesPagedRequest : IRequest<PagedQueryResult<CompanyDto>>
    {
        public PagedQueryRequest Query { get; set; }
    }

    public class GetCompaniesPagedRequestHandler : IRequestHandler<GetCompaniesPagedRequest, PagedQueryResult<CompanyDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityContext _identity;
        public GetCompaniesPagedRequestHandler(HeavyEquipmentContext context,
            IIdentityContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<CompanyDto>>> HandleAsync(GetCompaniesPagedRequest request)
        {
            var result = new PagedQueryResult<CompanyDto>();

            if (_identity.RequestingUser.CurrentUser != null)
            {
                var companyList = (from companies in _context.Company
                                   join assignedUser in _context.CompanyAssignedUser
                                    on companies.CompanyId equals assignedUser.CompanyId
                                   where assignedUser.UserId == _identity.RequestingUser.CurrentUser.UserId
                                   select companies)
                    .OrderBy(x => x.CompanyId)
                    .Distinct();

                int resultCount = companyList.Count(); // Store this since we call it twice below

                result.Items = Mapper.Map<List<CompanyDto>>(companyList
                        .Skip((request.Query.PageNumber - 1) * request.Query.PageSize)
                        .Take(request.Query.PageSize)
                        .ToList());

                result.PageCount = resultCount / request.Query.PageSize;
                result.TotalItemCount = resultCount;
                result.PageNumber = request.Query.PageNumber;
                result.PageSize = request.Query.PageSize;
            }

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
