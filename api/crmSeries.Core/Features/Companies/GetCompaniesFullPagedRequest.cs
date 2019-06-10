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
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Features.CompanyAssignedCategories.Dtos;
using crmSeries.Core.Features.CompanyAssignedRanks.Dtos;

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
            var companies = new List<CompanyFullDto>();
            var result = new PagedQueryResult<CompanyFullDto>();

            if (_identity.RequestingUser.UserId != 0)
            {
                var companyTotalList =
                    (from company in _context.Company
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
                     where
                         assignedUser.UserId == _identity.RequestingUser.UserId
                         && !company.Deleted
                     select new
                     {
                         company,
                         branch,
                         source,
                         recordType,
                         parentCompany
                     })
                .GroupJoin(
                    (from assignedRank in _context.CompanyAssignedRank
                     join rank in _context.CompanyRank
                        on assignedRank.RankId equals rank.RankId
                     join role in _context.UserRole
                        on assignedRank.RoleId equals role.RoleId
                     where !rank.Deleted && !role.Deleted
                     select new GetCompanyAssignedRankDto
                     {
                         AssignedId = assignedRank.AssignedId,
                         CompanyId = assignedRank.CompanyId,
                         RankId = assignedRank.RankId,
                         RoleId = assignedRank.RoleId,
                         Rank = rank.Rank,
                         Role = role.Role
                     }),
                    company => company.company.CompanyId,
                    rank => rank.CompanyId,
                    (x, y) => new
                    {
                        Details = x,
                        Ranks = y
                    }
                )
                .GroupJoin(
                    (from assignedCategory in _context.CompanyAssignedCategory
                     join category in _context.CompanyCategory
                        on assignedCategory.CategoryId equals category.CategoryId
                     where !category.Deleted
                     select new GetCompanyAssignedCategoryDto
                     {
                         AssignedId = assignedCategory.AssignedId,
                         CompanyId = assignedCategory.CompanyId,
                         CategoryId = category.CategoryId,
                         Category = category.Category
                     }),
                    company => company.Details.company.CompanyId,
                    rank => rank.CompanyId,
                    (x, y) => new
                    {
                        x.Details.company.ParentId,
                        x.Details.company.RecordTypeId,
                        x.Details.company.BranchId,
                        x.Details.company.CompanyName,
                        x.Details.company.LegalName,
                        x.Details.company.AccountNo,
                        x.Details.company.Address1,
                        x.Details.company.Address2,
                        x.Details.company.Address3,
                        x.Details.company.City,
                        x.Details.company.State,
                        x.Details.company.Zip,
                        x.Details.company.County,
                        x.Details.company.Mailing,
                        x.Details.company.Latitude,
                        x.Details.company.Longitude,
                        x.Details.company.Phone,
                        x.Details.company.Fax,
                        x.Details.company.Web,
                        x.Details.company.Linked,
                        x.Details.company.SourceId,
                        x.Details.company.Status,
                        x.Details.company.CompanyId,
                        x.Details.company.Deleted,
                        x.Details.company.LastModified,
                        Branch = x.Details.branch.BranchName,
                        x.Details.source.Source,
                        x.Details.recordType.RecordType,
                        ParentName = x.Details.parentCompany.CompanyName,
                        x.Ranks,
                        Categories = y
                    })
                    .OrderBy(x => x.CompanyId);

                int resultCount = companyTotalList.Count();

                companies = companyTotalList
                    .Skip((request.Query.PageNumber - 1) * request.Query.PageSize)
                    .Take(request.Query.PageSize)
                    .GroupJoin(
                        _context.CompanyAssignedAddress,
                        company => company.CompanyId,
                        address => address.CompanyId,
                        (x, y) => new
                        {
                            Details = x,
                            Addresses = y.Where(a => !a.Deleted)
                        }
                    )
                    .GroupJoin(
                        _context.Contact,
                        company => company.Details.CompanyId,
                        contact => contact.CompanyId,
                        (x, y) => new
                        {
                            x.Details,
                            x.Addresses,
                            Contacts = y.Where(c => c.Active && !c.Deleted)
                        }
                    )
                    .ProjectTo<CompanyFullDto>()
                    .ToList();

                var favorites = _context.UserFavoriteRecord
                    .Where(x =>
                        x.RecordType == Constants.RelatedRecord.Types.Company &&
                        x.UserId == _identity.RequestingUser.UserId)
                    .Select(x => x.RecordId)
                    .ToList();

                companies.ForEach(x => { x.Details.Favorite = favorites.Contains(x.Details.CompanyId); });

                result.Items = companies;
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
