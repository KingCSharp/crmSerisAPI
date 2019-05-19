using crmSeries.Core.Data;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using crmSeries.Core.Features.CompanyAssignedAddresses.Dtos;
using crmSeries.Core.Features.Contacts.Dtos;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetAllCompaniesFullPagedRequest : IRequest<PagedQueryResult<CompanyFullDto>>
    {
        public PagedQueryRequest Query { get; set; }
    }

    public class GetAllCompaniesFullPagedRequestHandler :
        IRequestHandler<GetAllCompaniesFullPagedRequest, PagedQueryResult<CompanyFullDto>>
    {
        private readonly HeavyEquipmentContext _context;
        public GetAllCompaniesFullPagedRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<CompanyFullDto>>> HandleAsync(GetAllCompaniesFullPagedRequest request)
        {
            var companyList = _context.Company.AsQueryable();
            int resultCount = companyList.Count();

            var resultList = companyList.Skip((request.Query.PageNumber - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Select(company => new CompanyFullDto
                {
                    Details = Mapper.Map<CompanyDto>(company),
                    Addresses = Mapper.Map<List<CompanyAssignedAddressDto>>(
                        _context.CompanyAssignedAddress
                        .Where(x => x.CompanyId == company.CompanyId).ToList()),
                    Contacts = Mapper.Map<List<GetContactDto>>(
                        _context.Contact
                        .Where(x => x.CompanyId == company.CompanyId).ToList())
                })
                .ToList();

            return new PagedQueryResult<CompanyFullDto>
            {
                Items = resultList,
                PageCount = resultCount / request.Query.PageSize,
                TotalItemCount = resultCount,
                PageNumber = request.Query.PageNumber,
                PageSize = request.Query.PageSize
            }.AsResponseAsync();
        }
    }

    public class GetAllCompaniesFullPagedValidator : AbstractValidator<GetAllCompaniesFullPagedRequest>
    {
        public GetAllCompaniesFullPagedValidator()
        {
            RuleFor(x => x.Query.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.Query.PageSize)
                .GreaterThan(0);
        }
    }
}
