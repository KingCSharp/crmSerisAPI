using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using crmSeries.Core.Features.CompanyAssignedRanks.Utility;

namespace crmSeries.Core.Features.CompanyAssignedRanks
{
    [HeavyEquipmentContext]
    public class DeleteCompanyAssignedRankRequest : IRequest
    {
        public DeleteCompanyAssignedRankRequest(int id)
        {
            CompanyAssignedRankId = id;
        }
        public int CompanyAssignedRankId { get; private set; }
    }

    public class DeleteCompanyAssignedRankHandler : IRequestHandler<DeleteCompanyAssignedRankRequest>
    {
        private readonly HeavyEquipmentContext _context;

        public DeleteCompanyAssignedRankHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(DeleteCompanyAssignedRankRequest request)
        {
            var companyAssignedRank = _context.CompanyAssignedRank
                .SingleOrDefault(x => x.AssignedId == request.CompanyAssignedRankId);

            if (companyAssignedRank == null)
                return Response.ErrorAsync(
                    CompanyAssignedRanksConstants.ErrorMessages.CompanyAssignedRankNotFound);

            _context.CompanyAssignedRank.Remove(companyAssignedRank);
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }

    public class DeleteCompanyAssignedRankValidator : AbstractValidator<DeleteCompanyAssignedRankRequest>
    {
        public DeleteCompanyAssignedRankValidator()
        {
            RuleFor(x => x.CompanyAssignedRankId).GreaterThan(0);
        }
    }
}