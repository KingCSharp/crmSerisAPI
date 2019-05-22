using AutoMapper;
using AutoMapper.QueryableExtensions;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Features.CompanyAssignedAddresses.Dtos;
using crmSeries.Core.Features.Contacts.Dtos;
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
            var companiesList = new List<CompanyFullDto>();

            if (_identity.RequestingUser.CurrentUser != null)
            {
                var companies =
                    (from company in _context.Company
                     join assignedUser in _context.CompanyAssignedUser
                     on company.CompanyId equals assignedUser.CompanyId
                     where assignedUser.UserId == _identity.RequestingUser.CurrentUser.UserId
                     && !company.Deleted
                     select company)
                     .ProjectTo<GetCompanyDto>()
                     .OrderBy(x => x.CompanyId)
                     .Distinct()
                     .ToList();

                var favorites = _context.UserFavoriteRecord
                .Where(x =>
                    x.RecordType == Constants.UserFavoriteRecords.Types.Company &&
                    x.UserId == _identity.RequestingUser.CurrentUser.UserId)
                .Select(x => x.RecordId)
                .ToList();

                foreach (var company in companies)
                {
                    var companyDto = new CompanyFullDto
                    {
                        Details = company,
                        Addresses = _context.CompanyAssignedAddress
                            .ProjectTo<CompanyAssignedAddressDto>()
                            .Where(x => x.CompanyId == company.CompanyId && !x.Deleted)
                            .ToList(),
                        Contacts = _context.Contact
                            .Where(x => x.CompanyId == company.CompanyId && !x.Deleted && x.Active)
                            .Select(x => new
                            {
                                x.ContactId,
                                x.CompanyId,
                                x.FirstName,
                                x.MiddleName,
                                x.LastName,
                                x.NickName,
                                x.Phone,
                                x.Cell,
                                x.Fax,
                                x.Email,
                                x.Title,
                                x.Position,
                                x.Department,
                                x.LastModified,
                                company.CompanyName,
                                company.AccountNo
                            })
                            .ProjectTo<GetContactDto>()
                            .ToList()
                    };
                    companyDto.Details.Favorite = favorites.Contains(company.CompanyId);
                    companiesList.Add(companyDto);
                }
            }

            return companiesList.AsEnumerable().AsResponseAsync();
        }
    }
}
