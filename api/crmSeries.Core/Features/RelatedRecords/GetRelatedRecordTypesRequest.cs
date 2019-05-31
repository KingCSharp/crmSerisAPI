using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmSeries.Core.Features.RelatedRecords
{
    [DoNotValidate]
    public class GetRelatedRecordTypesRequest : IRequest<IEnumerable<string>>
    {
    }

    public class GetNoteTypesHandler : IRequestHandler<GetRelatedRecordTypesRequest, IEnumerable<string>>
    {
        private static readonly IReadOnlyCollection<string> _relatedRecordTypes = 
            Constants.RelatedRecord.Types.ValidTypes;

        public Task<Response<IEnumerable<string>>> HandleAsync(GetRelatedRecordTypesRequest request)
        {
            return _relatedRecordTypes.AsEnumerable().AsResponseAsync();
        }
    }
}
