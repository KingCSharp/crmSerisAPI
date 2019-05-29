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

            var companyTotalList = (from company in _context.Company
                    join assignedUser in _context.CompanyAssignedUser
                        on company.CompanyId equals assignedUser.CompanyId
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
                    where assignedUser.UserId == _identity.RequestingUser.UserId
                          && !company.Deleted
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
                        Branch = branch.BranchName,
                        source.Source,
                        recordType.RecordType,
                        ParentName = parentCompany.CompanyName
                    })
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
                    x.RecordType == Constants.RelatedRecord.Types.Company &&
                    x.UserId == _identity.RequestingUser.UserId)
                .Select(x => x.RecordId)
                .ToList();

            result.Items = companyList;
            result.Items
                .Where(x => favorites.Contains(x.CompanyId))
                .ToList()
                .ForEach(x => { x.Favorite = true; });

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
