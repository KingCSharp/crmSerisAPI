using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetAllCompaniesPagedRequest : IRequest<PagedQueryResult<CompanyDto>>
    {
        public PagedQueryRequest Query { get; set; }
    }

    public class GetAllCompaniesPagedRequestHandler : IRequestHandler<GetAllCompaniesPagedRequest, PagedQueryResult<CompanyDto>>
    {
        private readonly HeavyEquipmentContext _context;
        public GetAllCompaniesPagedRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<CompanyDto>>> HandleAsync(GetAllCompaniesPagedRequest request)
        {
            var companyList = _context.Company.AsQueryable();
            int resultCount = companyList.Count();

            return new PagedQueryResult<CompanyDto>()
            {
                Items = Mapper.Map<List<CompanyDto>>(companyList
                 .Skip((request.Query.PageNumber - 1) * request.Query.PageSize)
                 .Take(request.Query.PageSize)
                 .ToList()),
                PageCount = resultCount / request.Query.PageSize,
                TotalItemCount = resultCount,
                PageNumber = request.Query.PageNumber,
                PageSize = request.Query.PageSize
            }.AsResponseAsync();
        }
    }

    public class GetAllCompaniesPagedValidator : AbstractValidator<GetAllCompaniesPagedRequest>
    {
        public GetAllCompaniesPagedValidator()
        {
            RuleFor(x => x.Query.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.Query.PageSize)
                .GreaterThan(0);
        }
    }
}
