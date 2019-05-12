using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies.Dtos;
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
    public class GetCompaniesFullRequest : IRequest<IEnumerable<CompanyFull>>
    {
    }

    public class GetCompaniesFullRequestHandler : IRequestHandler<GetCompaniesFullRequest, IEnumerable<CompanyFull>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityContext _identity;
        public GetCompaniesFullRequestHandler(HeavyEquipmentContext context,
            IIdentityContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<IEnumerable<CompanyFull>>> HandleAsync(GetCompaniesFullRequest request)
        {
            var companiesList = new List<CompanyFull>();

            if (_identity.RequestingUser.CurrentUser != null)
            {
                var companies =
                    (from company in _context.Company
                     join assignedUser in _context.CompanyAssignedUser
                     on company.CompanyId equals assignedUser.CompanyId
                     join user in _context.User
                     on assignedUser.UserId equals user.UserId
                     where user.UserId == _identity.RequestingUser.CurrentUser.UserId
                     select company)
                     .OrderBy(x => x.CompanyId)
                     .Distinct();

                foreach (var company in companies)
                {
                    companiesList.Add(new CompanyFull
                    {
                        Details = company,
                        Addresses = _context.CompanyAssignedAddress.Where(x => x.CompanyId == company.CompanyId).ToList(),
                        Contacts = _context.Contact.Where(x => x.CompanyId == company.CompanyId).ToList()
                    });
                }
            }

            return companiesList.AsEnumerable().AsResponseAsync();
        }
    }
}
