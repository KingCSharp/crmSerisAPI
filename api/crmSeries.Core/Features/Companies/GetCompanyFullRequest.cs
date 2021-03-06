using AutoMapper;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.CompanyAssignedAddresses.Dtos;
using crmSeries.Core.Features.CompanyAssignedCategories.Dtos;
using crmSeries.Core.Features.CompanyAssignedRanks.Dtos;
using crmSeries.Core.Features.Contacts.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompanyFullRequest : IRequest<CompanyFullDto>
    {
        public int CompanyId { get; set; }
    }

    public class GetCompanyFullRequestHandler : IRequestHandler<GetCompanyFullRequest, CompanyFullDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetCompanyFullRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<CompanyFullDto>> HandleAsync(GetCompanyFullRequest request)
        {
            var companyResult =
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
                    }
                )
                .ProjectTo<GetCompanyDto>()
                .SingleOrDefault(x => x.CompanyId == request.CompanyId && !x.Deleted);

            if (companyResult == null)
                return Response<CompanyFullDto>.ErrorAsync(new Error
                {
                    ErrorMessage = CompaniesConstants.ErrorMessages.CompanyNotFound
                });

            return new CompanyFullDto
            {
                Details = companyResult,
                Addresses = _context.CompanyAssignedAddress
                    .ProjectTo<CompanyAssignedAddressDto>()
                    .Where(x => x.CompanyId == companyResult.CompanyId && !x.Deleted)
                    .ToList(),
                Contacts = _context.Contact
                    .Where(x => x.CompanyId == companyResult.CompanyId && !x.Deleted && x.Active)
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
                        companyResult.CompanyName,
                        companyResult.AccountNo
                    })
                    .ProjectTo<GetContactDto>()
                    .ToList()
            }.AsResponseAsync();
        }
    }

    public class GetCompanyFullValidator : AbstractValidator<GetCompanyFullRequest>
    {
        public GetCompanyFullValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty();

            RuleFor(x => x.CompanyId)
                .GreaterThan(0);
        }
    }
}
