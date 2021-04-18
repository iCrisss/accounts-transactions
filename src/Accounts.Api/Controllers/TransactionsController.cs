using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.Features.Transactions.Report;
using Accounts.Api.Features.Transactions.Report.Models;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers
{
    [ApiController]
    [Route("~/api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private GetTransactionsReport _getTransactionsReport;
        public TransactionsController(GetTransactionsReport getTransactionsReport) 
        {
            _getTransactionsReport = getTransactionsReport;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<TransactionsPerCategoryAggregationModel>> Report([FromQuery]GetTransactionsReportInput input)
        {
            return await _getTransactionsReport.GetAccountTransactionsReport(input);
        }
    }
}