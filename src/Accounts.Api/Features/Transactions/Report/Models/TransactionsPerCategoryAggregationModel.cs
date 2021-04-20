namespace Accounts.Api.Features.Transactions.Report.Models
{
    public class TransactionsPerCategoryAggregationModel
    {
        public string CategoryName { get; set; }
        public double TotalAmount { get; set; }
        public string Currency { get; set; }
    }
}