using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.DataAccess.Transactions.Models;

namespace Accounts.Api.DataAccess.Transactions
{
    public class MockTransactionsRepo : ITransactionsRepo
    {
        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            /*
            Mock transactions for the following:
            - IBAN = NL69INGB0123456789
                - February 2020
                    - Food - 20
                    - Entertainment - 20
                    - Clothing - 20
                    - Travel - 20
                    - Medical expenses - 20
                - March 2020
                    - Food - 120
                    - Entertainment - 99.9
                    - Clothing - 150.5
                    - Travel - 0
                    - Medical expenses - 49.99
                - April 2020
                    - Food - 30.1
                    - Entertainment - 30.1
                    - Clothing - 30.1
                    - Travel - 30.1
                    - Medical expenses - 30.1
            - IBAN = NL69INGB0123456788
                - February 2020
                    - Food - 33.01
                - March 2020
                    - Food - 32.01
                - April 2020
                    - Food - 31.01
            - IBAN = RO69INGB0123456788
                - February 2020
                    - Food - 56.02
                - March 2020
                    - Food - 57.02
                - April 2020
                    - Food - 61.02
            */
            return await Task.FromResult(new List<Transaction> {
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 1,
                    Amount = 20,
                    CategoryId = 1,
                    TransactionDate = new DateTime(2021, 3, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 2,
                    Amount = 100,
                    CategoryId = 1,
                    TransactionDate = new DateTime(2021, 3, 15)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 3,
                    Amount = 33.3,
                    CategoryId = 2,
                    TransactionDate = new DateTime(2021, 3, 15)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 4,
                    Amount = 66.6,
                    CategoryId = 2,
                    TransactionDate = new DateTime(2021, 3, 25)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 5,
                    Amount = 150.50,
                    CategoryId = 3,
                    TransactionDate = new DateTime(2021, 3, 01)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 6,
                    Amount = 20,
                    CategoryId = 5,
                    TransactionDate = new DateTime(2021, 3, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 7,
                    Amount = 29.99,
                    CategoryId = 5,
                    TransactionDate = new DateTime(2021, 3, 11)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 8,
                    Amount = 20,
                    CategoryId = 1,
                    TransactionDate = new DateTime(2021, 2, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 9,
                    Amount = 20,
                    CategoryId = 2,
                    TransactionDate = new DateTime(2021, 2, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 10,
                    Amount = 20,
                    CategoryId = 3,
                    TransactionDate = new DateTime(2021, 2, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 11,
                    Amount = 20,
                    CategoryId = 4,
                    TransactionDate = new DateTime(2021, 2, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 12,
                    Amount = 20,
                    CategoryId = 5,
                    TransactionDate = new DateTime(2021, 2, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 13,
                    Amount = 30.1,
                    CategoryId = 1,
                    TransactionDate = new DateTime(2021, 4, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 14,
                    Amount = 30.1,
                    CategoryId = 2,
                    TransactionDate = new DateTime(2021, 4, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 15,
                    Amount = 30.1,
                    CategoryId = 3,
                    TransactionDate = new DateTime(2021, 4, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 16,
                    Amount = 30.1,
                    CategoryId = 4,
                    TransactionDate = new DateTime(2021, 4, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456789",
                    TransactionId = 17,
                    Amount = 30.1,
                    CategoryId = 5,
                    TransactionDate = new DateTime(2021, 4, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456788",
                    TransactionId = 18,
                    Amount = 33.01,
                    CategoryId = 1,
                    TransactionDate = new DateTime(2021, 2, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456788",
                    TransactionId = 19,
                    Amount = 32.01,
                    CategoryId = 1,
                    TransactionDate = new DateTime(2021, 3, 10)
                },
                new Transaction {
                    Iban = "NL69INGB0123456788",
                    TransactionId = 20,
                    Amount = 31.01,
                    CategoryId = 1,
                    TransactionDate = new DateTime(2021, 4, 10)
                },
                new Transaction {
                    Iban = "RO69INGB0123456788",
                    TransactionId = 21,
                    Amount = 56.01,
                    CategoryId = 1,
                    TransactionDate = new DateTime(2021, 2, 10)
                },
                new Transaction {
                    Iban = "RO69INGB0123456788",
                    TransactionId = 22,
                    Amount = 57.01,
                    CategoryId = 1,
                    TransactionDate = new DateTime(2021, 3, 10)
                },
                new Transaction {
                    Iban = "RO69INGB0123456788",
                    TransactionId = 23,
                    Amount = 61.01,
                    CategoryId = 1,
                    TransactionDate = new DateTime(2021, 4, 10)
                },
            });
        }
    }
}