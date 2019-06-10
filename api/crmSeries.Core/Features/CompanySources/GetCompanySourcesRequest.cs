using crmSeries.Core.Data;
using crmSeries.Core.Mediator.Decorators;
using System.Linq;
using crmSeries.Core.Mediator;
using crmSeries.Core.Features.CompanySources.Dtos;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Mediator.Attributes;
using System.Collections.Generic;

namespace crmSeries.Core.Features.CompanySources
{
    [DoNotValidate]
    [HeavyEquipmentContext]
    public class GetCompanySourcesRequest : IRequest<IEnumerable<GetCompanySourceDto>>
    {
    }

    public class GetCompanySourcesHandler :
        IRequestHandler<GetCompanySourcesRequest, IEnumerable<GetCompanySourceDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetCompanySourcesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<GetCompanySourceDto>>> HandleAsync(GetCompanySourcesRequest request)
        {
            return _context.CompanySource
                .ProjectTo<GetCompanySourceDto>()
                .AsEnumerable()
                .AsResponseAsync();
        }
    }
}
