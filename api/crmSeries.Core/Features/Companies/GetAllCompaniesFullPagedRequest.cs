using crmSeries.Core.Data;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetAllCompaniesFullPagedRequest : IRequest<PagedQueryResult<CompanyFull>>
    {
        public PagedQueryRequest Query { get; set; }
    }

    public class GetAllCompaniesFullPagedRequestHandler :
        IRequestHandler<GetAllCompaniesFullPagedRequest, PagedQueryResult<CompanyFull>>
    {
        private readonly HeavyEquipmentContext _context;
        public GetAllCompaniesFullPagedRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<CompanyFull>>> HandleAsync(GetAllCompaniesFullPagedRequest request)
        {
            var resultList = new List<CompanyFull>();
            var companyList = _context.Company.AsEnumerable();
            int resultCount = companyList.Count(); // Store this since we call it twice below

            foreach (var company in
                companyList
                .Skip((request.Query.PageNumber - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize))
            {
                resultList.Add(new CompanyFull
                {
                    Details = company,
                    Addresses = _context.CompanyAssignedAddress.Where(x => x.CompanyId == company.CompanyId).ToList(),
                    Contacts = _context.Contact.Where(x => x.CompanyId == company.CompanyId).ToList()
                });
            }

            return new PagedQueryResult<CompanyFull>
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
