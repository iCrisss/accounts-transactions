using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Api.DataAccess.Accounts;
using Accounts.Api.DataAccess.Accounts.Models;
using Accounts.Api.DataAccess.Transactions;
using Accounts.Api.DataAccess.Transactions.Models;
using Accounts.Api.Features.Transactions.Models;
using Accounts.Api.Features.Transactions.Report.Models;
using Accounts.Api.Utils;

namespace Accounts.Api.Features.Transactions.Report
{

    public interface IGetTransactionsReport 
    {
        Task<Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>> GetAccountTransactionsReport(GetTransactionsReportInput input);
    }

    public class GetTransactionsReport : IGetTransactionsReport
    {
        private IAccountsRepo _accountsRepo;
        private ITransactionsRepo _transactionsRepo;
        private IDateTimeProxy _dateTimeProxy;

        public GetTransactionsReport(IAccountsRepo accountsRepo, ITransactionsRepo transactionsRepo, IDateTimeProxy dateTimeProxy)
        {
            _accountsRepo = accountsRepo;
            _transactionsRepo = transactionsRepo;
            _dateTimeProxy = dateTimeProxy;
        }

        public async Task<Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>> GetAccountTransactionsReport(GetTransactionsReportInput input)
        {
            if(input == null) 
            {
                return Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Fail(GetTransactionsReportStatus.InputNull, "Input is null.");
            }

            if (String.IsNullOrEmpty(input.ClientId))
            {
                return Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Fail(GetTransactionsReportStatus.ClientIdNullOrEmpty, "ClientId is null or empty.");
            }
            
            if (String.IsNullOrEmpty(input.AccountResourceId))
            {
                return Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Fail(GetTransactionsReportStatus.AccountResourceIdNullOrEmpty, "AccountResourceId is null or empty.");
            }

            IEnumerable<Account> accounts = await _accountsRepo.GetAccounts(input.ClientId);
            var account = accounts.SingleOrDefault(a => a.ResourceId == input.AccountResourceId);
            if (account == null)
            {
                return Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Fail(GetTransactionsReportStatus.AccountNotFound, "No account could be found.");
            }

            //TODO: Retrieve only transactions from required month
            IEnumerable<Transaction> transactions = await _transactionsRepo.GetTransactions(account.Iban);
            transactions = transactions.Where(t => IsFromLastMonth(t.TransactionDate));

            if (transactions == null || !transactions.Any())
            {
                return Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Fail(GetTransactionsReportStatus.TransactionsForLastMonthNotFound, "No transactions found for last month");
            }

            var transactionsSummedByCategory = new Dictionary<int, TransactionsPerCategoryAggregationModel>();
            foreach (Transaction transaction in transactions)
            {
                if (transactionsSummedByCategory.ContainsKey(transaction.CategoryId))
                {
                    transactionsSummedByCategory[transaction.CategoryId].TotalAmount += transaction.Amount;
                }
                else
                {
                    transactionsSummedByCategory[transaction.CategoryId] = new TransactionsPerCategoryAggregationModel
                    {
                        CategoryName = ((TransactionCategory)transaction.CategoryId).ToString(),
                        TotalAmount = transaction.Amount,
                        Currency = account.Currency
                    };
                }
            }

            return Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Success(GetTransactionsReportStatus.Success, transactionsSummedByCategory.Values);
        }

        private Boolean IsFromLastMonth(DateTime transactionDate)
        {
            var currentDateTime = _dateTimeProxy.UtcNow();
            var startDate = new DateTime(currentDateTime.Year, currentDateTime.AddMonths(-1).Month, 1);
            var endDate = new DateTime(currentDateTime.Year, currentDateTime.Month, 1);

            return transactionDate >= startDate && transactionDate < endDate;
        }

    }
}