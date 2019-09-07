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
using crmSeries.Core.Security;

namespace crmSeries.Core.Features.UserFavoriteRecords
{
    [HeavyEquipmentContext]
    public class ToggleUserFavoriteRecordRequest : AddUserFavoriteRecordDto, IRequest<AddResponse>
    {
    }

    public class ToggleUserFavoriteRecordHandler : IRequestHandler<ToggleUserFavoriteRecordRequest, AddResponse>
    {
        private readonly HeavyEquipmentContext _context;
        private readonly IRequestHandler<VerifyRelatedRecordRequest> _verifyRelatedRecordsHandler;
        private readonly IRequestHandler<AddUserFavoriteRecordRequest, AddResponse> _addUserFavoriteRecordHandler;
        private readonly IRequestHandler<DeleteUserFavoriteRecordRequest> _deleteUserFavoriteRecordHandler;
        private readonly IIdentityUserContext _identityUserContext;
        private int _userId = 0;

        public ToggleUserFavoriteRecordHandler(HeavyEquipmentContext context,
            IRequestHandler<VerifyRelatedRecordRequest> verifyRelatedRecordsHandler,
            IRequestHandler<AddUserFavoriteRecordRequest, AddResponse> addUserFavoriteRecordHandler,
            IRequestHandler<DeleteUserFavoriteRecordRequest> deleteUserFavoriteRecordHandler,
            IIdentityUserContext identityUserContext)
        {
            _context = context;
            _verifyRelatedRecordsHandler = verifyRelatedRecordsHandler;
            _addUserFavoriteRecordHandler = addUserFavoriteRecordHandler;
            _deleteUserFavoriteRecordHandler = deleteUserFavoriteRecordHandler;
            _identityUserContext = identityUserContext;
        }

        public Task<Response<AddResponse>> HandleAsync(ToggleUserFavoriteRecordRequest request)
        {
            _userId = request.UserId == default(int) ? _identityUserContext.RequestingUser.UserId : request.UserId;

            if (!IsValid(request, out var errorAsync))
                return errorAsync;

            int existingRecordId = 0;

            if (FavoriteRecordExists(request, out existingRecordId))
            {
                _deleteUserFavoriteRecordHandler
                    .HandleAsync(new DeleteUserFavoriteRecordRequest(existingRecordId));

                return new AddResponse
                {
                    Id = 0
                }.AsResponseAsync();
            }
            else
            {
                var response = _addUserFavoriteRecordHandler.HandleAsync(new AddUserFavoriteRecordRequest
                {
                    RecordId = request.RecordId,
                    RecordType = request.RecordType,
                    UserId = _userId
                });

                var addResponse = new AddResponse
                {
                    Id = response.Result.Data.Id
                };
                
                return addResponse.AsResponseAsync();   
            }
        }

        private bool FavoriteRecordExists(ToggleUserFavoriteRecordRequest request, 
            out int existingRecordId)
        {
            var record = _context.UserFavoriteRecord.SingleOrDefault(x => x.RecordType == request.RecordType
                && x.RecordId == request.RecordId
                && x.UserId == _userId);

            existingRecordId = record != null ? record.FavoriteId : 0;

            return record != null;
        }

        private bool IsValid(AddUserFavoriteRecordDto request, out Task<Response<AddResponse>> errorAsync)
        {
            var relatedEntities = new List<(string, int)>
            {
                (request.RecordType, request.RecordId),
                (Constants.RelatedRecord.Types.User, _userId)
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

    public class ToggleUserFavoriteRecordValidator : AbstractValidator<ToggleUserFavoriteRecordRequest>
    {
        public ToggleUserFavoriteRecordValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(-1);
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