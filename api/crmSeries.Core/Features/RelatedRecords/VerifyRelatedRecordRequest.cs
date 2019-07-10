using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.CompanyCategories.Utility;
using crmSeries.Core.Features.CompanyRanks.Utility;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.Inventory.Utility;
using crmSeries.Core.Features.Leads.Utility;
using crmSeries.Core.Features.Notes.Utility;
using crmSeries.Core.Features.Opportunities.Utility;
using crmSeries.Core.Features.OutputTemplateCategories.Utility;
using crmSeries.Core.Features.OutputTemplateFields.Utility;
using crmSeries.Core.Features.OutputTemplates.Utility;
using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Features.UserRoles.Utility;
using crmSeries.Core.Features.Users.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using static crmSeries.Core.Features.RelatedRecords.Constants;

namespace crmSeries.Core.Features.RelatedRecords
{
    [HeavyEquipmentContext]
    public class VerifyRelatedRecordRequest : IRequest
    {
        public string RecordType { get; set; }

        public int RecordTypeId { get; set; }
    }

    public class VerifyRelatedRecordHandler : IRequestHandler<VerifyRelatedRecordRequest>
    {
        private readonly HeavyEquipmentContext _context;

        public VerifyRelatedRecordHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(VerifyRelatedRecordRequest request)
        {
            if (request.RecordTypeId == 0)
                return Response.SuccessAsync();

            if (request.RecordType == RelatedRecord.Types.Company)
            {
                if (!_context.Set<Company>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.CompanyId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(CompaniesConstants.ErrorMessages.CompanyNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Contact)
            {
                if (!_context.Set<Contact>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.ContactId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(ContactsConstants.ErrorMessages.ContactNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Equipment)
            {
                if (!_context.Set<Domain.HeavyEquipment.Equipment>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.EquipmentId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(InventoryConstants.ErrorMessages.InventoryNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Lead)
            {
                if (!_context.Set<Lead>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.LeadId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(LeadsConstants.ErrorMessages.LeadNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Opportunity)
            {
                if (!_context.Set<Opportunity>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.OpportunityId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(OpportunitiesConstants.ErrorMessages.OpportunityNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.User)
            {
                if (!_context.Set<User>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.UserId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(UsersConstants.ErrorMessages.UserNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Note)
            {
                if (!_context.Set<Note>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.NoteId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(NotesConstants.ErrorMessages.NoteNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Task)
            {
                if (!_context.Set<Domain.HeavyEquipment.Task>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.TaskId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(TasksConstants.ErrorMessages.TaskNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.CompanyCategory)
            {
                if (!_context.Set<CompanyCategory>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.CategoryId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(CompanyCategoriesConstants.ErrorMessages.CompanyCategoryNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.CompanyRank)
            {
                if (!_context.Set<CompanyRank>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.RankId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(CompanyRanksConstants.ErrorMessages.CompanyRankNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.UserRole)
            {
                if (!_context.Set<UserRole>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.RoleId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(UserRolesConstants.ErrorMessages.UserRoleNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.OutputTemplateCategory)
            {
                if (!_context.Set<OutputTemplateCategory>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.CategoryId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(OutputTemplateCategoriesConstants.ErrorMessages.OutputTemplateCategoryNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.OutputTemplate)
            {
                if (!_context.Set<OutputTemplate>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.TemplateId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(OutputTemplatesConstants.ErrorMessages.OutputTemplateNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.OutputTemplateField)
            {
                if (!_context.Set<OutputTemplateField>()
                    .AsNoTracking()
                    .RelatedEntityExists(x => x.FieldId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(OutputTemplateFieldsConstants.ErrorMessages.OutputTemplateFieldNotFound);
                }
            }

            return Response.SuccessAsync();
        }

        public class VerifyRelatedRecordValidator : AbstractValidator<VerifyRelatedRecordRequest>
        {
            public VerifyRelatedRecordValidator()
            {
                RuleFor(x => x.RecordTypeId).GreaterThan(0);
                RuleFor(x => x.RecordType).Must(BeAValidRelatedRecordType);
            }

            private bool BeAValidRelatedRecordType(string recordType)
            {
                return Constants.RelatedRecord.Types.ValidTypes.Any(x => x == recordType);
            }
        }
    }
}
