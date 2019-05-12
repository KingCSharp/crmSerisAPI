using System.Collections.Generic;
using System.Threading.Tasks;
using crmSeries.Api.Controllers;
using crmSeries.Core.Domain.HeavyEquipment;
using crmSeries.Core.Features.Companies;
using crmSeries.Core.Features.Companies.Dtos;
using crmSeries.Core.Logic.Queries;
using Microsoft.AspNetCore.Mvc;

namespace crmSeries.API.Controllers
{
    [Produces("application/json")]
    [Route("api/companies")]
    public class CompaniesController : BaseApiController
    {
        /// <summary>
        /// Gets a list of all companies assigned to the current user.
        /// </summary>
        // GET: api/companies
        [HttpGet]
        [Produces(typeof(IEnumerable<Company>))]
        public Task<IActionResult> GetCompanies()
        {
            return HandleAsync(new GetCompaniesRequest());
        }

        /// <summary>
        /// Gets a paged list of companies assigned to the current user.
        /// </summary>
        /// <param name="pagedQuery">Contains information for the current page and page count.</param>
        // GET: api/companies/paged
        [HttpGet]
        [Produces(typeof(PagedQueryResult<Company>))]
        [Route("paged")]
        public Task<IActionResult> GetPagedCompanies(PagedQueryRequest pagedQuery)
        {
            return HandleAsync(new GetCompaniesPagedRequest
            {
                Query = pagedQuery
            });
        }

        /// <summary>
        /// Gets a list of all companies assigned to the current user including all addresses and contacts.
        /// </summary>
        // GET: api/companies/full
        [HttpGet]
        [Produces(typeof(IEnumerable<CompanyFull>))]
        [Route("full")]
        public Task<IActionResult> GetCompaniesFull()
        {
            return HandleAsync(new GetCompaniesFullRequest());
        }

        /// <summary>
        /// Gets a paged list of all companies assigned to the current user including all addresses and contacts.
        /// </summary>
        /// <param name="pagedQuery">Contains information for the current page and page count.</param>
        // GET: api/companies/full/paged
        [HttpGet]
        [Produces(typeof(PagedQueryResult<CompanyFull>))]
        [Route("full/paged")]
        public Task<IActionResult> GetPagedCompaniesFull(PagedQueryRequest pagedQuery)
        {
            return HandleAsync(new GetCompaniesFullPagedRequest
            {
                Query = pagedQuery
            });
        }

        /// <summary>
        /// Gets a list of all companies.
        /// </summary>
        // GET: api/companies/all
        [HttpGet]
        [Produces(typeof(IEnumerable<Company>))]
        [Route("all")]
        public Task<IActionResult> GetAllCompanies()
        {
            return HandleAsync(new GetAllCompaniesRequest());
        }

        /// <summary>
        /// Gets a paged list of all companies.
        /// </summary>
        // GET: api/companies/all/paged
        [HttpGet]
        [Produces(typeof(PagedQueryResult<CompanyFull>))]
        [Route("all/paged")]
        public Task<IActionResult> GetPagedAllCompanies()
        {
            return HandleAsync(new GetAllCompaniesPagedRequest());
        }

        /// <summary>
        /// Gets a list of all companies including all addresses and contacts.
        /// </summary>
        // GET: api/companies/all/full
        [HttpGet]
        [Produces(typeof(IEnumerable<Company>))]
        [Route("all/full")]
        public Task<IActionResult> GetAllCompaniesFull()
        {
            return HandleAsync(new GetAllCompaniesFullRequest());
        }

        /// <summary>
        /// Gets a paged list of all companies including all addresses and contacts.
        /// </summary>
        // GET: api/companies/all/full/paged
        [HttpGet]
        [Produces(typeof(PagedQueryResult<CompanyFull>))]
        [Route("all/full/paged")]
        public Task<IActionResult> GetPagedAllCompaniesFull()
        {
            return HandleAsync(new GetAllCompaniesFullPagedRequest());
        }

        /// <summary>
        /// Gets a single company by id.
        /// </summary>
        /// <param name="id">The id of the company.</param>
        // GET: api/companies/id
        [HttpGet]
        [Produces(typeof(Company))]
        [Route("{id:int}")]
        public Task<IActionResult> GetCompany(int id)
        {
            return HandleAsync(new GetCompanyRequest
            {
                CompanyId = id
            });
        }

        /// <summary>
        /// Gets a single company by id including all addresses and contacts.
        /// </summary>
        /// <param name="id">The id of the company.</param>
        // GET: api/companies/id/full
        [HttpGet]
        [Produces(typeof(CompanyFull))]
        [Route("{id:int}/full")]
        public Task<IActionResult> GetCompanyFull(int id)
        {
            return HandleAsync(new GetCompanyFullRequest
            {
                CompanyId = id
            });
        }
    }
}