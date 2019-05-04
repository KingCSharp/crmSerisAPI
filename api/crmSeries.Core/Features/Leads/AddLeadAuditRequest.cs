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
            _context.RecordLog.Add(new RecordLog
            {
                RecordType =  WorkflowConstants.Modules.Lead,
                RecordId = request.Lead.LeadId,
                UserId = 0,

                // TODO - Do we need entered and created?
                Action = WorkflowConstants.ActionTypes.Entered,
                AssociatedRecordAction = "API",

                // TODO - Find out from Mark if he really wants this to work this way.
                OldValue = null,
                NewValue = null
            });

            _context.SaveChanges();
            return Response.SuccessAsync();
        }
    }
}
