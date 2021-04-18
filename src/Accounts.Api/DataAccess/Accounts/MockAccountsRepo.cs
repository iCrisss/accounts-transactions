using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.DataAccess.Accounts.Models;

namespace Accounts.Api.DataAccess.Accounts
{
    public class MockAccountsRepo : IAccountsRepo
    {
        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await Task.FromResult(new List<Account> {
                new Account {
                    ResourceId = Guid.NewGuid().ToString(),
                    Product = "Product1",
                    Iban = "NL69INGB0123456789",
                    Name = "AccountName1",
                    Currency = "EUR"
                },
                new Account {
                    ResourceId = Guid.NewGuid().ToString(),
                    Product = "Product2",
                    Iban = "NL69INGB0123456788",
                    Name = "AccountName2",
                    Currency = "EUR"
                },
                new Account {
                    ResourceId = Guid.NewGuid().ToString(),
                    Product = "Product3",
                    Iban = "RO69INGB0123456789",
                    Name = "AccountName3",
                    Currency = "EUR"
                },
                new Account {
                    ResourceId = Guid.NewGuid().ToString(),
                    Product = "Product4",
                    Iban = "RO69INGB0123456788",
                    Name = "AccountName4",
                    Currency = "RON"
                },
                new Account {
                    ResourceId = Guid.NewGuid().ToString(),
                    Product = "Product1",
                    Iban = "NL69INGB0123456787",
                    Name = "AccountName5",
                    Currency = "USD"
                },
            });
        }
    }
}