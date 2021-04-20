using System.ComponentModel.DataAnnotations;

namespace Accounts.Api.Features.Transactions.Report.Models
{
    public class GetTransactionsReportInput
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string AccountResourceId { get; set; }
    }
}