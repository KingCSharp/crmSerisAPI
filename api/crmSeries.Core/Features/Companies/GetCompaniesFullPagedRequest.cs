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

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompaniesFullPagedRequest : IRequest<PagedQueryResult<CompanyFull>>
    {
        public PagedQueryRequest Query { get; set; }
    }

    public class GetCompaniesFullPagedRequestHandler : 
        IRequestHandler<GetCompaniesFullPagedRequest, PagedQueryResult<CompanyFull>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityContext _identity;
        public GetCompaniesFullPagedRequestHandler(HeavyEquipmentContext context,
            IIdentityContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<CompanyFull>>> HandleAsync(GetCompaniesFullPagedRequest request)
        {
            var resultList = new List<CompanyFull>();
            var result = new PagedQueryResult<CompanyFull>();

            if (_identity.RequestingUser.CurrentUser != null)
            {
                var companyList = (from companies in _context.Company
                                   join assignedUser in _context.CompanyAssignedUser
                                    on companies.CompanyId equals assignedUser.CompanyId
                                   join user in _context.User
                                    on assignedUser.UserId equals user.UserId
                                   where user.UserId == _identity.RequestingUser.CurrentUser.UserId
                                   select companies)
                    .OrderBy(x => x.CompanyId)
                    .Distinct();

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
