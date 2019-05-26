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
            var company = _context.Company
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
