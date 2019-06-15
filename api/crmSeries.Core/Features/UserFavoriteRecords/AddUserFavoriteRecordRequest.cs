using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Core.Data;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Extensions;
using crmSeries.Core.Features.UserFavoriteRecords.Dtos;
using crmSeries.Core.Features.RelatedRecords;
using crmSeries.Core.Mediator;
using crmSeries.Core.Mediator.Decorators;
using FluentValidation;
using System.Linq;
using crmSeries.Core.Features.UserFavoriteRecords.Utility;

namespace crmSeries.Core.Features.UserFavoriteRecords
{
    [HeavyEquipmentContext]
    public class AddUserFavoriteRecordRequest : AddUserFavoriteRecordDto, IRequest<AddResponse>
    {
    }

    public class AddUserFavoriteRecordHandler : IRequestHandler<AddUserFavoriteRecordRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;

        public AddUserFavoriteRecordHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
        }

        public Task<Response<AddResponse>> HandleAsync(AddUserFavoriteRecordRequest request)
        {
            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            var userFavoriteRecord = request.MapTo<UserFavoriteRecord>();

            if (_context.UserFavoriteRecord.Any(x => x.RecordType == request.RecordType
                && x.RecordId == request.RecordId
                && x.UserId == request.UserId))
                return Response<AddResponse>
                    .ErrorAsync(UserFavoriteRecordsConstants.ErrorMessages.UserFavoriteRecordAlreadyExists);

            _context.Set<UserFavoriteRecord>().Add(userFavoriteRecord);
            _context.SaveChanges();

            return new AddResponse
            {
                Id = userFavoriteRecord.FavoriteId
            }.AsResponseAsync();
        }

        private bool IsValid(AddUserFavoriteRecordDto request, out Task<Response<AddResponse>> errorAsync)
        {
            var relatedEntities = new List<(string, int)>
            {
                (request.RecordType, request.RecordId),
                (Constants.RelatedRecord.Types.User, request.UserId)
            };

            foreach (var (relatedRecordType, relatedRecordTypeId) in relatedEntities)
            {
                var verifyRelatedRecordRequest = new VerifyRelatedRecordRequest
                {
                    RecordType = relatedRecordType,
                    RecordTypeId = relatedRecordTypeId
                };

                var result = _verifyRelatedRecordsHandler.HandleAsync(verifyRelatedRecordRequest).Result;

                if (result.HasErrors)
                {
                    errorAsync = Response<AddResponse>.ErrorsAsync(result.Errors);
                    return false;
                }
            }

            errorAsync = null;
            return true;
        }
    }

    public class AddUserFavoriteRecordValidator : AbstractValidator<AddUserFavoriteRecordRequest>
    {
        public AddUserFavoriteRecordValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.RecordId).GreaterThan(0);
            RuleFor(x => x.RecordType)
                .Must(BeAValidRelatedRecordType)
                .WithMessage(Constants.ErrorMessages.InvalidRecordType);
        }

        private bool BeAValidRelatedRecordType(string relatedRecordType)
        {
            return Constants.RelatedRecord.Types.ValidTypes.Any(x => x == relatedRecordType);
        }
    }
}