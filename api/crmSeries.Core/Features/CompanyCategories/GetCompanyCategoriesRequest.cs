using crmSeries.Core.Data;
using crmSeries.Core.Mediator.Decorators;
using System.Linq;
using crmSeries.Core.Mediator;
using crmSeries.Core.Features.CompanyCategories.Dtos;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Mediator.Attributes;
using System.Collections.Generic;

namespace crmSeries.Core.Features.CompanyCategories
{
    [DoNotValidate]
    [HeavyEquipmentContext]
    public class GetCompanyCategoriesRequest : IRequest<IEnumerable<GetCompanyCategoryDto>>
    {
    }

    public class GetCompanyCategoriesHandler :
        IRequestHandler<GetCompanyCategoriesRequest, IEnumerable<GetCompanyCategoryDto>>
    {
        private readonly HeavyEquipmentContext _context;

        public GetCompanyCategoriesHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<GetCompanyCategoryDto>>> HandleAsync(GetCompanyCategoriesRequest request)
        {
            return _context.CompanyCategory
                .Where(x => !x.Deleted)
                .ProjectTo<GetCompanyCategoryDto>()
                .AsEnumerable()
                .AsResponseAsync();
        }
    }
}
