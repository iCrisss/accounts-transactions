using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.DataAccess.Accounts.Models;

namespace Accounts.Api.DataAccess.Accounts
{
    public class MockAccountsRepo : IAccountsRepo
    {
        private static Dictionary<string, List<Account>> ClientAccounts = new Dictionary<string, List<Account>> {
            { "9488caed-e06c-4c15-b168-4c43369fcd49", new List<Account> {
                new Account {
                    ResourceId = "5aed4c3e-fd57-4c45-8f75-95787e52ebcf",
                    Product = "Product1",
                    Iban = "NL69INGB0123456789",
                    Name = "AccountName1",
                    Currency = "EUR"
                },
                new Account {
                    ResourceId = "f41f6853-b430-4fba-93ab-34efa5ac5348",
                    Product = "Product2",
                    Iban = "NL69INGB0123456788",
                    Name = "AccountName2",
                    Currency = "EUR"
                },
                new Account {
                    ResourceId = "702c3f24-9930-4439-837c-a6615dbf0661",
                    Product = "Product4",
                    Iban = "RO69INGB0123456788",
                    Name = "AccountName4",
                    Currency = "RON"
                },
            }
            },
            { "aaf224d3-8be5-452a-a284-8558d2d819c0", new List<Account> {
                new Account {
                    ResourceId = "6e043036-c051-4516-b5bb-faac6cd89503",
                    Product = "Product3",
                    Iban = "RO69INGB0123456789",
                    Name = "AccountName3",
                    Currency = "EUR"
                },
                new Account {
                    ResourceId = "f9fb1cd5-8c9d-4227-ab19-4f44edcf3efd",
                    Product = "Product1",
                    Iban = "NL69INGB0123456787",
                    Name = "AccountName5",
                    Currency = "USD"
                },
            }
            }
            };

        public async Task<IEnumerable<Account>> GetAccounts(string clientId)
        {
            return await Task.FromResult(ClientAccounts[clientId]);
        }
    }
}