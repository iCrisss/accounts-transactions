using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.DataAccess.Accounts;
using Accounts.Api.DataAccess.Accounts.Models;
using Accounts.Api.Features.Accounts.GetAccounts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers
{
    [ApiController]
    [Route("~/api/[controller]")]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private IAccountsRepo _accountsRepo;
        public AccountsController(IAccountsRepo accountsRepo) 
        {
            _accountsRepo = accountsRepo;
        }

        /// <summary>
        /// Retrieves the clients accounts
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/accounts?clientId=aaf224d3-8be5-452a-a284-8558d2d819c0
        ///
        /// </remarks>
        /// <returns>Array of client accounts</returns>
        /// <response code="200">Returns the array</response>
        /// <response code="400">If the ClientId is null</response>
        [HttpGet]
        public async Task<ActionResult<GetAccountsOutput>> Get([FromQuery]GetAccountsInput input) 
        {
            IEnumerable<Account> accounts = await _accountsRepo.GetAccounts(input.ClientId);

            return new GetAccountsOutput
            {
                Accounts = accounts
            };
        }
    }
}