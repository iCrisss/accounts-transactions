namespace Accounts.Api.Features.Transactions.Report.Models
{
    public class GetTransactionsReportInput
    {
        public string ClientId { get; set; }
        public string AccountResourceId { get; set; }
    }
}