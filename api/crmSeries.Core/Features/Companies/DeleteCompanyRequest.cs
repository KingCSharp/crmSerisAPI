using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using crmSeries.Core.Features.Companies.Utility;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class DeleteCompanyRequest : IRequest
    {
        public DeleteCompanyRequest(int id)
        {
            CompanyId = id;
        }
        public int CompanyId { get; private set; }
    }

    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyRequest>
    {
        private readonly HeavyEquipmentContext _context;

        public DeleteCompanyHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(DeleteCompanyRequest request)
        {
            var company = _context.Company.SingleOrDefault(x => x.CompanyId == request.CompanyId);

            if (company == null)
                return Response.ErrorAsync(CompaniesConstants.ErrorMessages.CompanyNotFound);

            company.Deleted = true;
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }

    public class DeleteCompanyValidator : AbstractValidator<DeleteCompanyRequest>
    {
        public DeleteCompanyValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0);
        }
    }
}