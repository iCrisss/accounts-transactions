using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.Features.Transactions.Report;
using Accounts.Api.Features.Transactions.Report.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers
{
    [ApiController]
    [Route("~/api/[controller]")]
    [Produces("application/json")]
    public class TransactionsController : ControllerBase
    {
        private GetTransactionsReport _getTransactionsReport;
        public TransactionsController(GetTransactionsReport getTransactionsReport)
        {
            _getTransactionsReport = getTransactionsReport;
        }

        /// <summary>
        /// Retrieves the sum of transactions from the previous month grouped by category for a specific account
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/transactions/report?clientId=9488caed-e06c-4c15-b168-4c43369fcd49&amp;accountResourceId=5aed4c3e-fd57-4c45-8f75-95787e52ebcf 
        ///
        /// </remarks>
        /// <returns>Array of transactions grouped by category</returns>
        /// <response code="200">Returns the array</response>
        /// <response code="400">If input is null, either ClientId or AccountResourceId</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<TransactionsPerCategoryAggregationModel>> Report([FromQuery] GetTransactionsReportInput input)
        {
            return await _getTransactionsReport.GetAccountTransactionsReport(input);
        }
    }
}