using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;

namespace crmSeries.Core.Features.Inventory
{
    [HeavyEquipmentContext]
    [DoNotValidate]
    public class GetInventoryStatusesRequest : IRequest<IEnumerable<string>>
    {
    }

    public class GetInventoryStatusesHandler : IRequestHandler<GetInventoryStatusesRequest, IEnumerable<string>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetInventoryStatusesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<string>>> HandleAsync(GetInventoryStatusesRequest request)
        {
            return _context.Set<Domain.HeavyEquipment.Equipment>()
                .Select(x => x.Status)
                .Where(x => x != "" && x != null)
                .Distinct()
                .OrderBy(x => x)
                .AsEnumerable()
                .AsResponseAsync();
        }
    }
}