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
    public class AccountsController : ControllerBase
    {
        private IAccountsRepo _accountsRepo;
        public AccountsController(IAccountsRepo accountsRepo) 
        {
            _accountsRepo = accountsRepo;
        }

        [HttpGet]
        public async Task<GetAccountsOutput> Get(string clientId) 
        {
            IEnumerable<Account> accounts = await _accountsRepo.GetAccounts(clientId);

            return new GetAccountsOutput
            {
                Accounts = accounts
            };
        }
    }
}