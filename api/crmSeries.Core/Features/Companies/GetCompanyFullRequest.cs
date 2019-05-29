using AutoMapper;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.CompanyAssignedAddresses.Dtos;
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
            var company = 
                (from cmp in _context.Company
                join joinedBranch in _context.Branch
                    on cmp.BranchId equals joinedBranch.BranchId into branchLeft
                from branch in branchLeft.DefaultIfEmpty()
                join joinedSource in _context.CompanySource
                    on cmp.SourceId equals joinedSource.SourceId into sourceLeft
                from source in sourceLeft.DefaultIfEmpty()
                join joinedRecordType in _context.CompanyRecordType
                    on cmp.RecordTypeId equals joinedRecordType.TypeId into recordTypeLeft
                from recordType in recordTypeLeft.DefaultIfEmpty()
                join joinedCompany in _context.Company
                    on cmp.ParentId equals joinedCompany.CompanyId into companyLeft
                from parentCompany in companyLeft.DefaultIfEmpty()
                select new
                {
                    cmp.ParentId,
                    cmp.RecordTypeId,
                    cmp.BranchId,
                    cmp.CompanyName,
                    cmp.LegalName,
                    cmp.AccountNo,
                    cmp.Address1,
                    cmp.Address2,
                    cmp.Address3,
                    cmp.City,
                    cmp.State,
                    cmp.Zip,
                    cmp.County,
                    cmp.Mailing,
                    cmp.Latitude,
                    cmp.Longitude,
                    cmp.Phone,
                    cmp.Fax,
                    cmp.Web,
                    cmp.Linked,
                    cmp.SourceId,
                    cmp.Status,
                    cmp.CompanyId,
                    cmp.Deleted,
                    cmp.LastModified,
                    Branch = branch.BranchName ?? string.Empty,
                    Source = source.Source ?? string.Empty,
                    RecordType = recordType.RecordType ?? string.Empty,
                    ParentName = parentCompany.CompanyName ?? string.Empty
                })
                .ProjectTo<GetCompanyDto>()
                .SingleOrDefault(x => x.CompanyId == request.CompanyId && !x.Deleted);

            if (company == null)
                return Response<CompanyFullDto>.ErrorAsync(new Error
                {
                    ErrorMessage = CompaniesConstants.ErrorMessages.CompanyNotFound
                });

            return new CompanyFullDto
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
