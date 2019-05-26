using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crmSeries.Core.Common;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.Companies.Utility;
using crmSeries.Core.Features.Contacts.Utility;
using crmSeries.Core.Features.Equipment.Utility;
using crmSeries.Core.Features.Leads.Utility;
using crmSeries.Core.Features.Notes.Utility;
using crmSeries.Core.Features.Opportunities.Utility;
using crmSeries.Core.Features.Tasks.Utility;
using crmSeries.Core.Features.Users.Utility;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using static crmSeries.Core.Common.Constants;

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
            if(request.RecordTypeId == 0)
                return Response.SuccessAsync();

            if (request.RecordType == RelatedRecord.Types.Company)
            {
                if (!_context.Set<Company>().RelatedEntityExists(x => x.CompanyId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(CompaniesConstants.ErrorMessages.CompanyNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Contact)
            {
                if (!_context.Set<Contact>().RelatedEntityExists(x => x.ContactId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(ContactsConstants.ErrorMessages.ContactNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Equipment)
            {
                if (!_context.Set<Domain.HeavyEquipment.Equipment> ().RelatedEntityExists(x => x.EquipmentId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(EquipmentConstants.ErrorMessages.EquipmentNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Lead)
            {
                if (!_context.Set<Lead>().RelatedEntityExists(x => x.LeadId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(LeadsConstants.ErrorMessages.InvalidLead);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Opportunity)
            {
                if (!_context.Set<Opportunity>().RelatedEntityExists(x => x.OpportunityId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(OpportunitiesConstants.ErrorMessages.OpportunityNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.User)
            {
                if (!_context.Set<User>().RelatedEntityExists(x => x.UserId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(UsersConstants.ErrorMessages.UserNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Note)
            {
                if (!_context.Set<Note>().RelatedEntityExists(x => x.NoteId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(NotesConstants.ErrorMessages.NoteNotFound);
                }
            }

            if (request.RecordType == RelatedRecord.Types.Task)
            {
                if (!_context.Set<Domain.HeavyEquipment.Task>().RelatedEntityExists(x => x.TaskId == request.RecordTypeId))
                {
                    return Response.ErrorAsync(TasksConstants.ErrorMessages.TaskNotFound);
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
