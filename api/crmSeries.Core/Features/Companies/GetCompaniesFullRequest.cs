using AutoMapper.QueryableExtensions;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    [DoNotValidate]
    public class GetCompaniesFullRequest : IRequest<IEnumerable<CompanyFullDto>>
    {
    }

    public class GetCompaniesFullRequestHandler : IRequestHandler<GetCompaniesFullRequest, IEnumerable<CompanyFullDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;
        public GetCompaniesFullRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<IEnumerable<CompanyFullDto>>> HandleAsync(GetCompaniesFullRequest request)
        {
            var companies = new List<CompanyFullDto>();

            if (_identity.RequestingUser.CurrentUser != null)
            {
                companies =
                    (from company in _context.Company
                     join assignedUser in _context.CompanyAssignedUser
                     on company.CompanyId equals assignedUser.CompanyId
                     where
                     assignedUser.UserId == _identity.RequestingUser.CurrentUser.UserId
                     && !company.Deleted
                     select company)
                    .OrderBy(x => x.CompanyId)
                    .GroupJoin(
                        _context.CompanyAssignedAddress,
                        company => company.CompanyId,
                        address => address.CompanyId,
                        (x, y) => new
                        {
                            Details = x,
                            Addresses = y.Where(a => !a.Deleted)
                        }
                    )
                    .GroupJoin(
                        _context.Contact,
                        company => company.Details.CompanyId,
                        contact => contact.CompanyId,
                        (x, y) => new
                        {
                            x.Details,
                            x.Addresses,
                            Contacts = y.Where(c => c.Active && !c.Deleted)
                        }
                    )
                    .ProjectTo<CompanyFullDto>()
                    .ToList();

                var favorites = _context.UserFavoriteRecord
                .Where(x =>
                    x.RecordType == Constants.UserFavoriteRecords.Types.Company &&
                    x.UserId == _identity.RequestingUser.CurrentUser.UserId)
                .Select(x => x.RecordId)
                .ToList();

                companies.ForEach(x =>
                {
                    x.Details.Favorite = favorites.Contains(x.Details.CompanyId);
                });
            }

            return companies.AsEnumerable().AsResponseAsync();
        }
    }
}
