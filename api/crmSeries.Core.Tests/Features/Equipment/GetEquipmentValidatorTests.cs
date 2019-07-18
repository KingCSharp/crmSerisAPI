using crmSeries.Core.Features.Equipment;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Tests.Extensions;
using NUnit.Framework;

namespace crmSeries.Core.Tests.Features.Equipment
{
    [TestFixture]
    public class GetEquipmentValidatorTests : BaseUnitTest
    {
        [Test]
        public void Validate_InvalidCategoryId_ReturnsInvalidResults()
        {
            var validator = CreateValidator();
            var request = CreateRequest();

            request.CategoryId = -1;

            var results = validator.Validate(request);

            results.AssertError(ExpectedError.ForGreaterThan("Category Id", -1));
        }

        [Test]
        public void Validate_InvalidPageNumber_ReturnsInvalidResults()
        {
            var validator = CreateValidator();
            var request = CreateRequest();

            request.PageNumber = 0;

            var results = validator.Validate(request);

            results.AssertError(ExpectedError.ForGreaterThan("Page Number", 0));
        }

        [Test]
        public void Validate_InvalidPageSize_ReturnsInvalidResults()
        {
            var validator = CreateValidator();
            var request = CreateRequest();

            request.PageSize = 0;

            var results = validator.Validate(request);

            results.AssertError(ExpectedError.ForGreaterThan("Page Size", 0));
        }

        [Test]
        public void Validate_InvalidParentType_ReturnsInvalidResults()
        {
            var validator = CreateValidator();
            var request = CreateRequest();

            request.ParentType = "Invalid";

            var results = validator.Validate(request);

            results.AssertError(EquipmentConstants.ErrorMessages.InvalidParentType);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Validate_ParentTypeNullOrEmpty_BypassesValidation(string parentType)
        {
            var validator = CreateValidator();
            var request = CreateRequest();

            request.ParentType = parentType;

            var results = validator.Validate(request);

            results.AssertIsValid();
        }

        [Test]
        public void Validate_InvalidStartHoursMin_ReturnsInvalidResults()
        {
            var validator = CreateValidator();
            var request = CreateRequest();

            request.StartHoursMin = 11;

            var results = validator.Validate(request);

            results.AssertError(ExpectedError.ForLessThanOrEqualTo("Start Hours Min", request.StartHoursMax));
        }

        //[Test]
        //public void Validate_StartHoursMinDefault_BypassesValidation()
        //{
        //    var validator = CreateValidator();
        //    var request = CreateRequest();

        //    request.StartHoursMin = default;

        //    var results = validator.Validate(request);

        //    results.AssertIsValid();
        //}

        [Test]
        public void Validate_ValidRequest_ReturnsValidResults()
        {
            var validator = CreateValidator();
            var request = CreateRequest();

            var results = validator.Validate(request);

            results.AssertIsValid();
        }

        private static GetEquipmentRequest CreateRequest() =>
            new GetEquipmentRequest
            {
                CategoryId = 1,
                PageSize = 1,
                PageNumber = 1,
                IsMachine = true,
                ParentType = EquipmentConstants.Company,
                StartHoursMax = 10,
                StartHoursMin = 1
            };

        private static GetEquipmentValidator CreateValidator() => new GetEquipmentValidator();
    }
}