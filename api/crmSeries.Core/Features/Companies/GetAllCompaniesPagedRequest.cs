using crmSeries.Core.Data;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetAllCompaniesPagedRequest : IRequest<PagedQueryResult<GetCompanyDto>>
    {
        public PagedQueryRequest Query { get; set; }
    }

    public class
        GetAllCompaniesPagedRequestHandler : IRequestHandler<GetAllCompaniesPagedRequest,
            PagedQueryResult<GetCompanyDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetAllCompaniesPagedRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<PagedQueryResult<GetCompanyDto>>> HandleAsync(GetAllCompaniesPagedRequest request)
        {
            var companyList =
                (from company in _context.Company
                 join joinedBranch in _context.Branch
                     on company.BranchId equals joinedBranch.BranchId into branchLeft
                 from branch in branchLeft.DefaultIfEmpty()
                 join joinedSource in _context.CompanySource
                     on company.SourceId equals joinedSource.SourceId into sourceLeft
                 from source in sourceLeft.DefaultIfEmpty()
                 join joinedRecordType in _context.CompanyRecordType
                     on company.RecordTypeId equals joinedRecordType.TypeId into recordTypeLeft
                 from recordType in recordTypeLeft.DefaultIfEmpty()
                 join joinedCompany in _context.Company
                     on company.ParentId equals joinedCompany.CompanyId into companyLeft
                 from parentCompany in companyLeft.DefaultIfEmpty()
                 select new
                 {
                     company.ParentId,
                     company.RecordTypeId,
                     company.BranchId,
                     company.CompanyName,
                     company.LegalName,
                     company.AccountNo,
                     company.Address1,
                     company.Address2,
                     company.Address3,
                     company.City,
                     company.State,
                     company.Zip,
                     company.County,
                     company.Mailing,
                     company.Latitude,
                     company.Longitude,
                     company.Phone,
                     company.Fax,
                     company.Web,
                     company.Linked,
                     company.SourceId,
                     company.Status,
                     company.CompanyId,
                     company.Deleted,
                     company.LastModified,
                     Branch = branch.BranchName ?? string.Empty,
                     Source = source.Source ?? string.Empty,
                     RecordType = recordType.RecordType ?? string.Empty,
                     ParentName = parentCompany.CompanyName ?? string.Empty
                 })
                .Where(x => !x.Deleted);
            int resultCount = companyList.Count();

            return new PagedQueryResult<GetCompanyDto>()
            {
                Items = companyList
                    .ProjectTo<GetCompanyDto>()
                    .Skip((request.Query.PageNumber - 1) * request.Query.PageSize)
                    .Take(request.Query.PageSize)
                    .ToList(),
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
