using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    [DoNotValidate]
    public class GetAllCompaniesRequest : IRequest<IEnumerable<Company>>
    {
    }

    public class GetAllCompaniesRequestHandler : IRequestHandler<GetAllCompaniesRequest, IEnumerable<Company>>
    {
        private readonly HeavyEquipmentContext _context;
        public GetAllCompaniesRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<Company>>> HandleAsync(GetAllCompaniesRequest request)
        {
            return _context.Company
                .AsEnumerable()
                .AsResponseAsync();
        }
    }
}
