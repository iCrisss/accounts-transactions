using System.Collections.Generic;
using Accounts.Api.DataAccess.Accounts.Models;

namespace Accounts.Api.Features.Accounts.GetAccounts.Models
{
    public class GetAccountsOutput
    {
        public IEnumerable<Account> Accounts { get; set; }
    }
}