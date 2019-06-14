﻿using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Features.Equipment;
using crmSeries.Core.Logic.Queries;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/equipment")]
    public class EquipmentController : BaseApiController
    {
        /// <summary>
        /// Gets equipment based on search criteria. 
        /// </summary>
        [HttpGet]
        [Produces(typeof(PagedQueryResult<GetEquipmentDto>))]
        public Task<IActionResult> Get(GetEquipmentRequest request)
        {
            return HandleAsync(request);
        }

        /// <summary>
        /// Gets equipment statuses for this dealer. 
        /// </summary>
        [HttpGet("statuses")]
        [Produces(typeof(IEnumerable<string>))]
        public Task<IActionResult> GetStatuses()
        {
            return HandleAsync(new GetEquipmentStatusesRequest());
        }
    }
}