using System.ComponentModel.DataAnnotations;

namespace Accounts.Api.Features.Accounts.GetAccounts.Models
{
    public class GetAccountsInput
    {
        [Required]
        public string ClientId { get; set; }
    }
}