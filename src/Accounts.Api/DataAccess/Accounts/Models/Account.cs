namespace Accounts.Api.DataAccess.Accounts.Models
{
    public class Account
    {
        public string ResourceId { get; set; }
        public string Product { get; set; }
        public string Iban { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
    }
}