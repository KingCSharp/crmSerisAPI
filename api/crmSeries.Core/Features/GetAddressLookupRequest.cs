using crmSeries.Core.Mediator;
using System.Threading.Tasks;
using crmSeries.Core.Mediator.Decorators;
using System.Linq;
using System.Collections.Generic;
using crmSeries.Core.Domain.Admin;
using crmSeries.Core.Data;

namespace crmSeries.Core.Features
{
    [DoNotValidate]
    [AdminContext]
    public class GetAddressLookupRequest : IRequest<IEnumerable<AddressLookupValue>>
    {
    }

    public class GetAddressLookupRequestHandler : IRequestHandler<GetAddressLookupRequest, IEnumerable<AddressLookupValue>>
    {
        private readonly AdminContext _context;

        public GetAddressLookupRequestHandler(AdminContext context)
        {
            _context = context;
        }
        public Task<Response<IEnumerable<AddressLookupValue>>> HandleAsync(GetAddressLookupRequest request)
        {
            return _context.AddressLookupValue.Take(10).AsEnumerable().AsResponseAsync();
        }
    }
}
