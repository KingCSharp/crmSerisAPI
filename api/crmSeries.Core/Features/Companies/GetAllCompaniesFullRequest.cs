using crmSeries.Core.Data;
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
    public class GetAllCompaniesFullRequest : IRequest<IEnumerable<CompanyFull>>
    {
    }

    public class GetAllCompaniesFullRequestHandler : IRequestHandler<GetAllCompaniesFullRequest, IEnumerable<CompanyFull>>
    {
        private readonly HeavyEquipmentContext _context;
        public GetAllCompaniesFullRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<CompanyFull>>> HandleAsync(GetAllCompaniesFullRequest request)
        {
            var companiesList = new List<CompanyFull>();
            var companies = _context.Company.ToList();

            foreach (var company in companies)
            {
                companiesList.Add(new CompanyFull
                {
                    Details = company,
                    Addresses = _context.CompanyAssignedAddress.Where(x => x.CompanyId == company.CompanyId).ToList(),
                    Contacts = _context.Contact.Where(x => x.CompanyId == company.CompanyId).ToList()
                });
            }

            return companiesList.AsEnumerable().AsResponseAsync();
        }
    }
}
