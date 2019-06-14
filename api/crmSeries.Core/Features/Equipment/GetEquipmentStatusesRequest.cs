using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;

namespace crmSeries.Core.Features.Equipment
{
    [HeavyEquipmentContext]
    [DoNotValidate]
    public class GetEquipmentStatusesRequest : IRequest<IEnumerable<string>>
    {
    }

    public class GetEquipmentStatusesHandler : IRequestHandler<GetEquipmentStatusesRequest, IEnumerable<string>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetEquipmentStatusesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<string>>> HandleAsync(GetEquipmentStatusesRequest request)
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