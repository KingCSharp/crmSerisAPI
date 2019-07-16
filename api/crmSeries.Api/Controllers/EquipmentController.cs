using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Equipment.Dtos;
using crmSeries.Core.Features.Notes;
using crmSeries.Core.Features.Notes.Dtos;
using crmSeries.Core.Logic.Queries;
using crmSeries.Core.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/equipment")]
    public class EquipmentController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all equipment 
        /// </summary>
        /// <param name="request">The request information for this API endpoint.</param>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetEquipmentDto>))]
        public Task<IActionResult> GetEquipment(GetEquipmentRequest request)
        {
            return HandleAsync(request);
        }
    }
}