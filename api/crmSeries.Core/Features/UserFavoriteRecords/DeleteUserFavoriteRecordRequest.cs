using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using crmSeries.Core.Features.UserFavoriteRecords.Utility;

namespace crmSeries.Core.Features.UserFavoriteRecords
{
    [HeavyEquipmentContext]
    public class DeleteUserFavoriteRecordRequest : IRequest
    {
        public DeleteUserFavoriteRecordRequest(int id)
        {
            UserFavoriteRecordId = id;
        }
        public int UserFavoriteRecordId { get; private set; }
    }

    public class DeleteUserFavoriteRecordHandler : IRequestHandler<DeleteUserFavoriteRecordRequest>
    {
        private readonly HeavyEquipmentContext _context;

        public DeleteUserFavoriteRecordHandler(HeavyEquipmentContext context)
        {
            _context = context;
        }

        public Task<Response> HandleAsync(DeleteUserFavoriteRecordRequest request)
        {
            var userFavoriteRecord = _context.UserFavoriteRecord
                .SingleOrDefault(x => x.FavoriteId == request.UserFavoriteRecordId);

            if (userFavoriteRecord == null)
                return Response.ErrorAsync(
                    UserFavoriteRecordsConstants.ErrorMessages.UserFavoriteRecordNotFound);

            _context.UserFavoriteRecord.Remove(userFavoriteRecord);
            _context.SaveChanges();

            return Response.SuccessAsync();
        }
    }

    public class DeleteUserFavoriteRecordValidator : AbstractValidator<DeleteUserFavoriteRecordRequest>
    {
        public DeleteUserFavoriteRecordValidator()
        {
            RuleFor(x => x.UserFavoriteRecordId).GreaterThan(0);
        }
    }
}