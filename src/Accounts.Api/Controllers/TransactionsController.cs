using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.DataAccess.Transactions;
using Accounts.Api.Features.Transactions.Report;
using Accounts.Api.Features.Transactions.Report.Models;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers
{
    [ApiController]
    [Route("~/api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private ITransactionsRepo _transactionsRepo;
        public TransactionsController(ITransactionsRepo transactionsRepo) 
        {
            _transactionsRepo = transactionsRepo;
        }
    }
}