using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using crmSeries.Core.Features.CompanyAssignedCategories.Utility;

namespace crmSeries.Core.Features.CompanyAssignedCategories
{
    [HeavyEquipmentContext]
    public class DeleteCompanyAssignedCategoryRequest : IRequest
    {
        public DeleteCompanyAssignedCategoryRequest(int id)
        {
            CompanyAssignedCategoryId = id;
        }
        public int CompanyAssignedCategoryId { get; private set; }
    }

    public class DeleteCompanyAssignedCategoryHandler : IRequestHandler<DeleteCompanyAssignedCategoryRequest>
    {
        private readonly HeavyEquipmentContext _context;

        public DeleteCompanyAssignedCategoryHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(DeleteCompanyAssignedCategoryRequest request)
        {
            var companyAssignedCategory = _context.CompanyAssignedCategory
                .SingleOrDefault(x => x.AssignedId == request.CompanyAssignedCategoryId);

            if (companyAssignedCategory == null)
                return Response.ErrorAsync(
                    CompanyAssignedCategoriesConstants.ErrorMessages.CompanyAssignedCategoryNotFound);

            _context.CompanyAssignedCategory.Remove(companyAssignedCategory);
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }

    public class DeleteCompanyAssignedCategoryHandlerValidator : AbstractValidator<DeleteCompanyAssignedCategoryRequest>
    {
        public DeleteCompanyAssignedCategoryHandlerValidator()
        {
            RuleFor(x => x.CompanyAssignedCategoryId).GreaterThan(0);
        }
    }
}