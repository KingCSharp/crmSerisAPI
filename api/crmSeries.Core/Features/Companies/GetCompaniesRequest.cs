using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompaniesRequest : IRequest<IEnumerable<Company>>
    {
        public string UserEmail { get; set; }
    }

    public class GetCompaniesRequestHandler : IRequestHandler<GetCompaniesRequest, IEnumerable<Company>>
    {
        private readonly HeavyEquipmentContext _context;
        public GetCompaniesRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<IEnumerable<Company>>> HandleAsync(GetCompaniesRequest request)
        {
            return (from companies in _context.Company
                    join assignedUser in _context.CompanyAssignedUser
                     on companies.CompanyId equals assignedUser.CompanyId
                    join user in _context.User
                     on assignedUser.UserId equals user.UserId
                    where user.Email == request.UserEmail
                    select companies)
                    .AsEnumerable()
                    .AsResponseAsync();
        }
    }

    public class GetCompaniesValidator : AbstractValidator<GetCompaniesRequest>
    {
        public GetCompaniesValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotEmpty();
        }
    }
}
