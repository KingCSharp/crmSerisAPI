using crmSeries.Api.Controllers;
using crmSeries.Core.Features.CompanyRecordTypes;
using crmSeries.Core.Features.CompanyRecordTypes.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crmSeries.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/companyrecordtypes")]
    public class CompanyRecordTypesController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all company record types.
        /// </summary>
        [HttpGet]
        [Produces(typeof(IEnumerable<GetCompanyRecordTypeDto>))]
        public Task<IActionResult> GetCompanyRecordTypes()
        {
            return HandleAsync(new GetCompanyRecordTypesRequest());
        }

        /// <summary>
        /// Gets a company record type entity based on the given id.
        /// </summary>
        /// <param name="id">The unique identifier of the company record type.</param>
        [HttpGet("{id}")]
        [Produces(typeof(GetCompanyRecordTypeDto))]
        public Task<IActionResult> GetCompanyRecordTypeById(int id)
        {
            return HandleAsync(new GetCompanyRecordTypeByIdRequest(id));
        }
    }
}
