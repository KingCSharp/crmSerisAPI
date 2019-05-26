using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Workflows;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using crmSeries.Core.Mediator.Decorators;

namespace crmSeries.Core.Features.Leads
{
    [HeavyEquipmentContext]
    [DoNotValidate]
    public class AddLeadAuditRequest : IRequest
    {
        public Lead Lead { get; set; }
    }

    public class AddLeadAuditRequestHandler : IRequestHandler<AddLeadAuditRequest>
    {
        private readonly HeavyEquipmentContext _context;

        public AddLeadAuditRequestHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(AddLeadAuditRequest request)
        {
            DateTime auditStamp = DateTime.UtcNow;

            var recordLogs = GetAndUpdate(
                request,
                auditStamp,
                x => x.Lead.Address1,
                x => x.Lead.Address2,
                x => x.Lead.Cell,
                x => x.Lead.City,
                x => x.Lead.Comments,
                x => x.Lead.CompanyId,
                x => x.Lead.CompanyName,
                x => x.Lead.Country,
                x => x.Lead.County,
                x => x.Lead.CompanyPhone,
                x => x.Lead.ContactId,
                x => x.Lead.DateAcknowledged,
                x => x.Lead.DateConverted,
                x => x.Lead.DateAssigned,
                x => x.Lead.Deleted,
                x => x.Lead.Department,
                x => x.Lead.Description,
                x => x.Lead.Email,
                x => x.Lead.Fax,
                x => x.Lead.FirstName,
                x => x.Lead.LastName,
                x => x.Lead.OwnerId,
                x => x.Lead.Phone,
                x => x.Lead.Position,
                x => x.Lead.State,
                x => x.Lead.SourceId,
                x => x.Lead.StatusId,
                x => x.Lead.Title,
                x => x.Lead.Web,
                x => x.Lead.Zip);

            _context.RecordLog.AddRange(recordLogs);
            _context.SaveChanges();

            return Response.SuccessAsync();
        }

        private RecordLog GetAndUpdate(AddLeadAuditRequest request, DateTime auditStamp, Func<AddLeadAuditRequest, object> accessor)
        {
            var log = GetRecordLog(request, auditStamp);
            log.NewValue = accessor(request).ToString();
            return log;
        }

        private IEnumerable<RecordLog> GetAndUpdate(AddLeadAuditRequest request,
            DateTime auditStamp,
            params Func<AddLeadAuditRequest, object>[] accessors)
        {
            foreach (var accessor in accessors)
                yield return GetAndUpdate(request, auditStamp, accessor);
        }

        private static RecordLog GetRecordLog(AddLeadAuditRequest request, DateTime stamp)
        {
            return new RecordLog
            {
                OldValue = null,
                RecordType = WorkflowConstants.Modules.Lead,
                RecordId = request.Lead.LeadId,
                UserId = 0,
                Action = WorkflowConstants.ActionTypes.Entered,
                AssociatedRecordAction = "API",
                TimeStamp = stamp
            };
        }
    }
}