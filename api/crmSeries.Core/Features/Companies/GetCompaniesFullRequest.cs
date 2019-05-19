using AutoMapper;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Features.CompanyAssignedAddresses.Dtos;
using crmSeries.Core.Features.Contacts.Dtos;
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
            var companiesList = new List<CompanyFullDto>();

            if (_identity.RequestingUser.CurrentUser != null)
            {
                var companies =
                    (from company in _context.Company
                     join assignedUser in _context.CompanyAssignedUser
                     on company.CompanyId equals assignedUser.CompanyId
                     where assignedUser.UserId == _identity.RequestingUser.CurrentUser.UserId
                     select company)
                     .OrderBy(x => x.CompanyId)
                     .Distinct();

                foreach (var company in companies)
                {
                    companiesList.Add(new CompanyFullDto
                    {
                        Details = Mapper.Map<CompanyDto>(company),
                        Addresses = Mapper.Map<List<CompanyAssignedAddressDto>>(
                            _context.CompanyAssignedAddress
                            .Where(x => x.CompanyId == company.CompanyId)
                            .ToList()),
                        Contacts = Mapper.Map<List<GetContactDto>>(
                            _context.Contact
                            .Where(x => x.CompanyId == company.CompanyId)
                            .ToList())
                    });
                }
            }

            return companiesList.AsEnumerable().AsResponseAsync();
        }
    }
}
