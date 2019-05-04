using crmSeries.Core.Features.Workflows;
using crmSeries.Core.Validation;
using NUnit.Framework;
using static crmSeries.Core.Features.Workflows.ExecuteWorkflowRuleHandler;

namespace crmSeries.Tests.Core.Features.Workflows
{
    [TestFixture]
    public class ExecuteWorkflowValidatorTests
    {
        private ExecuteWorkflowValidator _executeWorkflowValidator;
        private ExecuteWorkflowRuleRequest _executeWorkflowRuleRequest;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _executeWorkflowValidator = new ExecuteWorkflowValidator();
        }

        [SetUp]
        public void Setup()
        {
            _executeWorkflowRuleRequest = new ExecuteWorkflowRuleRequest
            {
                EntityId = 1,
                ActionType = WorkflowConstants.ActionTypes.Created,
                Module = WorkflowConstants.Modules.Lead
            };
        }

        [TestCase("Doesn't Exist", false, ErrorMessages.ExecuteWorkflowRuleRequest.ModuleInvalid)]
        [TestCase("lead", false, ErrorMessages.ExecuteWorkflowRuleRequest.ModuleInvalid)]
        [TestCase("Lead", true)]
        public void Validate_ModuleIsInvalid_ReturnsAppropriateErrorMessage(
            string module,
            bool isValid,
            string errorMessage = null)
        {
            // Arrange 
            _executeWorkflowRuleRequest.Module = module;

            // Act
            var result = _executeWorkflowValidator.Validate(_executeWorkflowRuleRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
                Assert.AreEqual(errorMessage, result.Errors[0].ErrorMessage);
        }

        [TestCase("Doesn't Exist", false, ErrorMessages.ExecuteWorkflowRuleRequest.ActionTypeInvalid)]
        [TestCase("created", false, ErrorMessages.ExecuteWorkflowRuleRequest.ActionTypeInvalid)]
        [TestCase("Created", true)]
        public void Validate_ActionTypeIsInvalid_ReturnsAppropriateErrorMessage(
            string actionType,
            bool isValid,
            string errorMessage = null)
        {
            // Arrange 
            _executeWorkflowRuleRequest.ActionType = actionType;

            // Act
            var result = _executeWorkflowValidator.Validate(_executeWorkflowRuleRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
                Assert.AreEqual(errorMessage, result.Errors[0].ErrorMessage);
        }

        [TestCase(0, false)]
        [TestCase(193, true)]
        public void Validate_EntityIdIsZero_ReturnsInvalidWithAnErrorMessage(
            int entityId,
            bool isValid)
        {
            // Arrange 
            _executeWorkflowRuleRequest.EntityId = entityId;

            // Act
            var result = _executeWorkflowValidator.Validate(_executeWorkflowRuleRequest);

            //Assert 
            Assert.AreEqual(isValid, result.IsValid);

            if (!result.IsValid)
                Assert.IsTrue(!string.IsNullOrEmpty(result.Errors[0].ErrorMessage));
        }
    }
}
