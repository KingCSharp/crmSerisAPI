using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    [DoNotValidate]
    public class GetCompaniesFullRequest : IRequest<IEnumerable<CompanyFullDto>>
    {
    }

    public class GetCompaniesFullRequestHandler : IRequestHandler<GetCompaniesFullRequest, IEnumerable<CompanyFullDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;

        public GetCompaniesFullRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<IEnumerable<CompanyFullDto>>> HandleAsync(GetCompaniesFullRequest request)
        {
            var companies = new List<CompanyFullDto>();

            if (_identity.RequestingUser.UserId != 0)
            {
                companies =
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
                    .OrderBy(x => x.CompanyId)
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
            }

            return companies.AsEnumerable().AsResponseAsync();
        }
    }
}
