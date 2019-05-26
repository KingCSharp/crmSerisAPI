using crmSeries.Core.Data;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Features.CompanyAssignedAddresses.Dtos;
using crmSeries.Core.Features.Contacts.Dtos;
using AutoMapper.QueryableExtensions;

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
            var companiesList = new List<CompanyFullDto>();
            var companies = _context.Company.AsQueryable();
            int resultCount = companies.Count();

            var resultList = companies
                .ProjectTo<GetCompanyDto>()
                .Where(x => !x.Deleted)
                .Skip((request.Query.PageNumber - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .ToList();

            foreach (var company in resultList)
            {
                var companyDto = new CompanyFullDto
                {
                    Details = company,
                    Addresses = _context.CompanyAssignedAddress
                        .ProjectTo<CompanyAssignedAddressDto>()
                        .Where(x => x.CompanyId == company.CompanyId && !x.Deleted)
                        .ToList(),
                    Contacts = _context.Contact
                        .Where(x => x.CompanyId == company.CompanyId && !x.Deleted && x.Active)
                        .Select(x => new
                        {
                            x.ContactId,
                            x.CompanyId,
                            x.FirstName,
                            x.MiddleName,
                            x.LastName,
                            x.NickName,
                            x.Phone,
                            x.Cell,
                            x.Fax,
                            x.Email,
                            x.Title,
                            x.Position,
                            x.Department,
                            x.Active,
                            x.LastModified,
                            company.CompanyName,
                            company.AccountNo
                        })
                        .ProjectTo<GetContactDto>()
                        .ToList()
                };
                companiesList.Add(companyDto);
            }

            return new PagedQueryResult<CompanyFullDto>
            {
                Items = companiesList,
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
