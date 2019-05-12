using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
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
    public class GetCompaniesRequest : IRequest<IEnumerable<Company>>
    {
    }

    public class GetCompaniesRequestHandler : IRequestHandler<GetCompaniesRequest, IEnumerable<Company>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityContext _identity;
        public GetCompaniesRequestHandler(HeavyEquipmentContext context,
            IIdentityContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<IEnumerable<Company>>> HandleAsync(GetCompaniesRequest request)
        {
            IEnumerable<Company> companiesList = new List<Company>();

            if (_identity.RequestingUser.CurrentUser != null)
            {
                companiesList = (from companies in _context.Company
                                 join assignedUser in _context.CompanyAssignedUser
                                 on companies.CompanyId equals assignedUser.CompanyId
                                 join user in _context.User
                                 on assignedUser.UserId equals user.UserId
                                 where user.UserId == _identity.RequestingUser.CurrentUser.UserId
                                 select companies)
                    .OrderBy(x => x.CompanyId)
                    .Distinct()
                    .AsEnumerable();
            }

            return companiesList.AsResponseAsync();
        }
    }
}
