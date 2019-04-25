using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;

namespace crmSeries.Core.Features.Diagnostics
{
    [DoNotValidate]
    [HeavyEquipmentContext]
    public class GetDiagnosticsMessageRequest : IRequest<string>
    {
    }

    public class GetDiagnosticsMessageHandler : IRequestHandler<GetDiagnosticsMessageRequest, string>
    {
        private readonly HeavyEquipmentContext _context;

        public GetDiagnosticsMessageHandler(HeavyEquipmentContext contextProvider)
        {
            _context = contextProvider;
        }

        public Task<Response<string>> HandleAsync(GetDiagnosticsMessageRequest request)
        {
            var lead = _context.Lead.FirstOrDefault();

            return "I'm up and running.".AsResponseAsync();
        }
    }

}
