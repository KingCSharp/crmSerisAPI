using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Data;
using crmSeries.Core.Features.Branches.Dtos;
using crmSeries.Core.Features.Branches.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.Branches
{
    [HeavyEquipmentContext]
    public class GetBranchByIdRequest : IRequest<GetBranchDto>
    {
        public GetBranchByIdRequest(int branchId)
        {
            BranchId = branchId;
        }
        public int BranchId { get; private set; }
    }

    public class GetBranchByIdHandler : IRequestHandler<GetBranchByIdRequest, GetBranchDto>
    {
        private readonly HeavyEquipmentContext _context;

        public GetBranchByIdHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<GetBranchDto>> HandleAsync(GetBranchByIdRequest request)
        {
            var branch = _context.Branch
                .ProjectTo<GetBranchDto>()
                .SingleOrDefault(x => x.BranchId == request.BranchId && !x.Deleted);

            if (branch == null)
                return Response<GetBranchDto>
                    .ErrorAsync(BranchesConstants.ErrorMessages.BranchNotFound);

            return branch.AsResponseAsync();
        }
    }

    public class GetBranchByIdValidator : AbstractValidator<GetBranchByIdRequest>
    {
        public GetBranchByIdValidator()
        {
            RuleFor(x => x.BranchId).GreaterThan(0);
        }
    }
}