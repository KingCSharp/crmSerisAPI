using AutoMapper;
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
                .SingleOrDefault(x => x.CompanyId == request.CompanyId);

            if (company == null)
                throw new NullReferenceException(CompaniesConstants.ErrorMessages.CompanyIdNotValid);

            return new CompanyFullDto
            {
                Details = Mapper.Map<CompanyDto>(company),
                Addresses = Mapper.Map<List<CompanyAssignedAddressDto>>(
                    _context.CompanyAssignedAddress
                    .Where(x => x.CompanyId == company.CompanyId)
                    .ToList()),
                Contacts = Mapper.Map<List<ContactDto>>(
                    _context.Contact
                    .Where(x => x.CompanyId == company.CompanyId)
                    .ToList())
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
