using System;

namespace Accounts.Api.DataAccess.Transactions.Models
{
    public class Transaction
    {
        public string Iban { get; set; }
        public long TransactionId { get; set; }
        public double Amount { get; set; }
        public int CategoryId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}