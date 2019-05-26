using crmSeries.Core.Data;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using crmSeries.Core.Security;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Common;
using AutoMapper.QueryableExtensions;

namespace crmSeries.Core.Features.Companies
{
    [HeavyEquipmentContext]
    public class GetCompaniesFullPagedRequest : IRequest<PagedQueryResult<CompanyFullDto>>
    {
        public PagedQueryRequest Query { get; set; }
    }

    public class GetCompaniesFullPagedRequestHandler :
        IRequestHandler<GetCompaniesFullPagedRequest, PagedQueryResult<CompanyFullDto>>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IIdentityUserContext _identity;

        public GetCompaniesFullPagedRequestHandler(HeavyEquipmentContext context,
            IIdentityUserContext identity)
        {
            _context = context;
            _identity = identity;
        }

        public Task<Response<PagedQueryResult<CompanyFullDto>>> HandleAsync(GetCompaniesFullPagedRequest request)
        {
            var companies = new List<CompanyFullDto>();
            var result = new PagedQueryResult<CompanyFullDto>();

            if (_identity.RequestingUser.UserId != 0)
            {
                var companyTotalList =
                    (from company in _context.Company
                        join assignedUser in _context.CompanyAssignedUser
                            on company.CompanyId equals assignedUser.CompanyId
                        where
                            assignedUser.UserId == _identity.RequestingUser.UserId
                            && !company.Deleted
                        select company)
                    .OrderBy(x => x.CompanyId);

                int resultCount = companyTotalList.Count();

                companies = companyTotalList
                    .Skip((request.Query.PageNumber - 1) * request.Query.PageSize)
                    .Take(request.Query.PageSize)
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
                        x.RecordType == Constants.RelatedRecord.Types.Company &&
                        x.UserId == _identity.RequestingUser.UserId)
                    .Select(x => x.RecordId)
                    .ToList();

                companies.ForEach(x => { x.Details.Favorite = favorites.Contains(x.Details.CompanyId); });

                result.Items = companies;
                result.PageCount = resultCount / request.Query.PageSize;
                result.TotalItemCount = resultCount;
                result.PageNumber = request.Query.PageNumber;
                result.PageSize = request.Query.PageSize;
            }

            return result.AsResponseAsync();
        }
    }

    public class GetCompaniesFullPagedValidator : AbstractValidator<GetCompaniesFullPagedRequest>
    {
        public GetCompaniesFullPagedValidator()
        {
            RuleFor(x => x.Query.PageNumber)
                .GreaterThan(0);

            RuleFor(x => x.Query.PageSize)
                .GreaterThan(0);
        }
    }
}
