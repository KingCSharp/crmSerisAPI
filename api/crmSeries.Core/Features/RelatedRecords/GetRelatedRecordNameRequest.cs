using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.Leads.Utility;
using crmSeries.Core.Features.Users.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;

namespace crmSeries.Core.Features.RelatedRecords
{
    [HeavyEquipmentContext]
    public class GetRelatedRecordNameRequest : IRequest<string>
    {
        public int RelatedRecordTypeId { get; private set; }

        public string RelatedRecordType { get; private set; }

        public GetRelatedRecordNameRequest(int relatedRecordTypeId, string relatedRecordType)
        {
            RelatedRecordTypeId = relatedRecordTypeId;
            RelatedRecordType = relatedRecordType;
        }
    }

    public class GetRelatedRecordNameHandler : IRequestHandler<GetRelatedRecordNameRequest, string>
    {
        private readonly HeavyEquipmentContext _context;

        public GetRelatedRecordNameHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response<string>> HandleAsync(GetRelatedRecordNameRequest request)
        {
            switch (request.RelatedRecordType)
            {
                case Constants.RelatedRecord.Types.Company:
                    return GetRelatedCompanyName(request.RelatedRecordTypeId);
                case Constants.RelatedRecord.Types.Contact:
                    return GetRelatedContactName(request.RelatedRecordTypeId);
                case Constants.RelatedRecord.Types.Lead:
                    return GetRelatedLeadName(request.RelatedRecordTypeId);
                case Constants.RelatedRecord.Types.User:
                    return GetRelatedUserName(request.RelatedRecordTypeId);
                default:
                    return Response<string>.ErrorAsync("This related record type is not supported");
            }
        }

        private Task<Response<string>> GetRelatedCompanyName(int relatedRecordTypeId)
        {
            if (!_context.Set<Company>()
                .RelatedEntityExists(x => x.CompanyId == relatedRecordTypeId))
            {
                return Response<string>.ErrorAsync(CompaniesConstants.ErrorMessages.CompanyNotFound);
            }

            var name = _context.Set<Company>()
                .Where(x => x.CompanyId == relatedRecordTypeId)
                .Select(x => x.CompanyName)
                .Single();

            return name.AsResponseAsync();
        }

        private Task<Response<string>> GetRelatedContactName(int relatedRecordTypeId)
        {
            if (!_context.Set<Contact>()
                .RelatedEntityExists(x => x.ContactId == relatedRecordTypeId))
            {
                return Response<string>.ErrorAsync(ContactsConstants.ErrorMessages.ContactNotFound);
            }

            var names = _context.Set<Contact>()
                .Where(x => x.ContactId == relatedRecordTypeId)
                .Select(x => new {x.FirstName, x.LastName})
                .Single();

            return $"{names.FirstName} {names.LastName}".AsResponseAsync();
        }

        private Task<Response<string>> GetRelatedLeadName(int relatedRecordTypeId)
        {
            if (!_context.Set<Lead>()
                .RelatedEntityExists(x => x.LeadId == relatedRecordTypeId))
            {
                return Response<string>.ErrorAsync(LeadsConstants.ErrorMessages.LeadNotFound);
            }

            var names = _context.Set<Lead>()
                .Where(x => x.LeadId == relatedRecordTypeId)
                .Select(x => new { x.FirstName, x.LastName })
                .Single();

            return $"{names.FirstName} {names.LastName}".AsResponseAsync();
        }

        private Task<Response<string>> GetRelatedUserName(int relatedRecordTypeId)
        {
            if (!_context.Set<User>()
                .RelatedEntityExists(x => x.UserId == relatedRecordTypeId))
            {
                return Response<string>.ErrorAsync(UsersConstants.ErrorMessages.UserNotFound);
            }

            var names = _context.Set<User>()
                .Where(x => x.UserId == relatedRecordTypeId)
                .Select(x => new { x.FirstName, x.LastName })
                .Single();

            return $"{names.FirstName} {names.LastName}".AsResponseAsync();
        }
    }

    public class GetRelatedRecordNameValidator : AbstractValidator<GetRelatedRecordNameRequest>
    {
        public GetRelatedRecordNameValidator()
        {
            RuleFor(x => x.RelatedRecordTypeId).GreaterThan(0);
            RuleFor(x => x.RelatedRecordType)
                .Must(BeAValidRelatedRecordType)
                .WithMessage(Constants.ErrorMessages.InvalidRecordType);
        }

        private bool BeAValidRelatedRecordType(string relatedRecordType)
        {
            return Constants.RelatedRecord.Types.ValidTypes.Any(x => x == relatedRecordType);
        }
    }
}
