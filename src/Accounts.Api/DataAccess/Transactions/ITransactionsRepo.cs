using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.DataAccess.Transactions.Models;

namespace Accounts.Api.DataAccess.Transactions
{
    public interface ITransactionsRepo
    {
        Task<IEnumerable<Transaction>> GetTransactionsFromLastMonth(string iban);
    }
}