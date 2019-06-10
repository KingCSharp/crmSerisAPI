using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Features.CompanyCategories.Dtos;
using crmSeries.Core.Features.CompanyCategories.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.CompanyCategories
{
    [HeavyEquipmentContext]
    public class GetCompanyCategoryByIdRequest : IRequest<GetCompanyCategoryDto>
    {
        public GetCompanyCategoryByIdRequest(int companyCategoryId)
        {
            CompanyCategoryId = companyCategoryId;
        }
        public int CompanyCategoryId { get; private set; }
    }

    public class GetCompanyCategoryByIdHandler : IRequestHandler<GetCompanyCategoryByIdRequest, GetCompanyCategoryDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetCompanyCategoryByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetCompanyCategoryDto>> HandleAsync(GetCompanyCategoryByIdRequest request)
        {
            var companyCategory = _context.CompanyCategory
                .ProjectTo<GetCompanyCategoryDto>()
                .SingleOrDefault(x => x.CategoryId == request.CompanyCategoryId && !x.Deleted);

            if (companyCategory == null)
                return Response<GetCompanyCategoryDto>
                    .ErrorAsync(CompanyCategoriesConstants.ErrorMessages.CompanyCategoryNotFound);

            return companyCategory.AsResponseAsync();
        }
    }

    public class GetCompanyCategoryByIdValidator : AbstractValidator<GetCompanyCategoryByIdRequest>
    {
        public GetCompanyCategoryByIdValidator()
        {
            RuleFor(x => x.CompanyCategoryId).GreaterThan(0);
        }
    }
}