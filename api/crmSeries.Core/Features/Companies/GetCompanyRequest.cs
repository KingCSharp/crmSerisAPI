using crmSeries.Core.Data;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompanyRequest : IRequest<GetCompanyDto>
    {
        public int CompanyId { get; set; }
    }

    public class GetCompanyRequestHandler : IRequestHandler<GetCompanyRequest, GetCompanyDto>
    {
        private readonly HeavyEquipmentContext _context;
        public GetCompanyRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetCompanyDto>> HandleAsync(GetCompanyRequest request)
        {
            return (from company in _context.Company
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
                        on company.ParentId equals joinedCompany.CompanyId  into companyLeft
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
                        Branch = branch.BranchName,
                        source.Source,
                        recordType.RecordType,
                        ParentName = parentCompany.CompanyName
                    })
                .ProjectTo<GetCompanyDto>()
                .SingleOrDefault(x => x.CompanyId == request.CompanyId && !x.Deleted)
                .AsResponseAsync();
        }
    }

    public class GetCompanyValidator : AbstractValidator<GetCompanyRequest>
    {
        public GetCompanyValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty();

            RuleFor(x => x.CompanyId)
                .GreaterThan(0);
        }
    }
}
