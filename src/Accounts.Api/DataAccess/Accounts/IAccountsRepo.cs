using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.DataAccess.Accounts.Models;

namespace Accounts.Api.DataAccess.Accounts
{
    public interface IAccountsRepo
    {
        Task<IEnumerable<Account>> GetAccounts(string clientId);
    }
}