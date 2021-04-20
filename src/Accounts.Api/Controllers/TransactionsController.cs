using System.Collections.Generic;
using System.Linq;
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
        private IGetTransactionsReport _getTransactionsReport;
        public TransactionsController(IGetTransactionsReport getTransactionsReport)
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
        /// <response code="404">If account or transactions for last month cannot be found</response>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TransactionsPerCategoryAggregationModel>>> Report([FromQuery] GetTransactionsReportInput input)
        {
            var result = await _getTransactionsReport.GetAccountTransactionsReport(input);

            if (!result.IsSuccess)
            {
                switch (result.Status)
                {
                    case GetTransactionsReportStatus.InputNull:
                    case GetTransactionsReportStatus.ClientIdNullOrEmpty:
                    case GetTransactionsReportStatus.AccountResourceIdNullOrEmpty:
                        return BadRequest(new ProblemDetails
                        {
                            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                            Title = "One or more validation errors occurred.",
                            Status = StatusCodes.Status400BadRequest,
                            Detail = result.Message
                        });
                    case GetTransactionsReportStatus.AccountNotFound:
                    case GetTransactionsReportStatus.TransactionsForLastMonthNotFound:
                        return NotFound(new ProblemDetails
                        {
                            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                            Title = "Resource could not be found.",
                            Status = StatusCodes.Status404NotFound,
                            Detail = result.Message
                        });
                }
            }

            return result.Value.ToList();
        }
    }
}