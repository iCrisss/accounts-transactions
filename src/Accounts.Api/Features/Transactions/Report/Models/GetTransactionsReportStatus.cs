namespace Accounts.Api.Features.Transactions.Report.Models
{
    public enum GetTransactionsReportStatus
    {
        Success,
        InputNull,
        ClientIdNullOrEmpty,
        AccountResourceIdNullOrEmpty,
        AccountNotFound,
        TransactionsForLastMonthNotFound
    }
}