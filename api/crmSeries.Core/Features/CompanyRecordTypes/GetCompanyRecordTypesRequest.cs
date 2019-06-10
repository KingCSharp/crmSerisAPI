using crmSeries.Core.Data;
using crmSeries.Core.Mediator.Decorators;
using System.Linq;
using crmSeries.Core.Mediator;
using crmSeries.Core.Features.CompanyRecordTypes.Dtos;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Mediator.Attributes;
using System.Collections.Generic;

namespace crmSeries.Core.Features.CompanyRecordTypes
{
    [DoNotValidate]
    [HeavyEquipmentContext]
    public class GetCompanyRecordTypesRequest : IRequest<IEnumerable<GetCompanyRecordTypeDto>>
    {
    }

    public class GetCompanyRecordTypesHandler :
        IRequestHandler<GetCompanyRecordTypesRequest, IEnumerable<GetCompanyRecordTypeDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetCompanyRecordTypesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<GetCompanyRecordTypeDto>>> HandleAsync(GetCompanyRecordTypesRequest request)
        {
            return _context.CompanyRecordType
                .Where(x => !x.Deleted)
                .ProjectTo<GetCompanyRecordTypeDto>()
                .AsEnumerable()
                .AsResponseAsync();
        }
    }
}
