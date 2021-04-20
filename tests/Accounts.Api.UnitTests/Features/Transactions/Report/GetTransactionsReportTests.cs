using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Api.DataAccess.Accounts;
using Accounts.Api.DataAccess.Accounts.Models;
using Accounts.Api.DataAccess.Transactions;
using Accounts.Api.DataAccess.Transactions.Models;
using Accounts.Api.Features.Transactions.Models;
using Accounts.Api.Features.Transactions.Report;
using Accounts.Api.Features.Transactions.Report.Models;
using Accounts.Api.Utils;
using Moq;
using Xunit;

namespace Accounts.Api.UnitTests.Features.Transactions.Report
{
    public class GetTransactionsReportTests
    {
        [Fact]
        public async Task GetAccountTransactionsReport_InputNull_ReturnsFailResultWithInputNullStatus()
        {
            //Given
            GetTransactionsReportInput input = null;
            //When
            var accountsRepoMock = new Mock<IAccountsRepo>();
            var transactionsRepoMock = new Mock<ITransactionsRepo>();
            var dateTimeProxyMock = new Mock<IDateTimeProxy>();

            var getTransactionsReport = new GetTransactionsReport(accountsRepoMock.Object, transactionsRepoMock.Object, dateTimeProxyMock.Object);
            var report = await getTransactionsReport.GetAccountTransactionsReport(input);
            //Then
            Assert.False(report.IsSuccess);
            Assert.Equal(GetTransactionsReportStatus.InputNull, report.Status);
        }

        [Fact]
        public async Task GetAccountTransactionsReport_ClientIdNull_ReturnsFailResultWithClientIdNullOrEmptyStatus()
        {
            //Given
            var input = new GetTransactionsReportInput
            {
                AccountResourceId = "test"
            };
            //When
            var accountsRepoMock = new Mock<IAccountsRepo>();
            var transactionsRepoMock = new Mock<ITransactionsRepo>();
            var dateTimeProxyMock = new Mock<IDateTimeProxy>();

            var getTransactionsReport = new GetTransactionsReport(accountsRepoMock.Object, transactionsRepoMock.Object, dateTimeProxyMock.Object);
            var report = await getTransactionsReport.GetAccountTransactionsReport(input);
            //Then
            Assert.False(report.IsSuccess);
            Assert.Equal(GetTransactionsReportStatus.ClientIdNullOrEmpty, report.Status);
        }

        [Fact]
        public async Task GetAccountTransactionsReport_InputClientIdEmpty_ReturnsFailResultWithNullOrEmptyStatus()
        {
            //Given
            var input = new GetTransactionsReportInput
            {
                ClientId = String.Empty,
                AccountResourceId = "test"
            };
            //When
            var accountsRepoMock = new Mock<IAccountsRepo>();
            var transactionsRepoMock = new Mock<ITransactionsRepo>();
            var dateTimeProxyMock = new Mock<IDateTimeProxy>();

            var getTransactionsReport = new GetTransactionsReport(accountsRepoMock.Object, transactionsRepoMock.Object, dateTimeProxyMock.Object);
            var report = await getTransactionsReport.GetAccountTransactionsReport(input);
            //Then
            Assert.False(report.IsSuccess);
            Assert.Equal(GetTransactionsReportStatus.ClientIdNullOrEmpty, report.Status);
        }

        [Fact]
        public async Task GetAccountTransactionsReport_AccountResourceIdNull_ReturnsFailResultWithAccountResourceIdNullOrEmptyStatus()
        {
            //Given
            var input = new GetTransactionsReportInput
            {
                ClientId = "test"
            };
            //When
            var accountsRepoMock = new Mock<IAccountsRepo>();
            var transactionsRepoMock = new Mock<ITransactionsRepo>();
            var dateTimeProxyMock = new Mock<IDateTimeProxy>();

            var getTransactionsReport = new GetTransactionsReport(accountsRepoMock.Object, transactionsRepoMock.Object, dateTimeProxyMock.Object);
            var report = await getTransactionsReport.GetAccountTransactionsReport(input);
            //Then
            Assert.False(report.IsSuccess);
            Assert.Equal(GetTransactionsReportStatus.AccountResourceIdNullOrEmpty, report.Status);
        }

        [Fact]
        public async Task GetAccountTransactionsReport_AccountResourceIdEmpty_ReturnsFailResultWithAccountResourceIdNullOrEmptyStatus()
        {
            //Given
            var input = new GetTransactionsReportInput
            {
                ClientId = "test",
                AccountResourceId = String.Empty
            };
            //When
            var accountsRepoMock = new Mock<IAccountsRepo>();
            var transactionsRepoMock = new Mock<ITransactionsRepo>();
            var dateTimeProxyMock = new Mock<IDateTimeProxy>();

            var getTransactionsReport = new GetTransactionsReport(accountsRepoMock.Object, transactionsRepoMock.Object, dateTimeProxyMock.Object);
            var report = await getTransactionsReport.GetAccountTransactionsReport(input);
            //Then
            Assert.False(report.IsSuccess);
            Assert.Equal(GetTransactionsReportStatus.AccountResourceIdNullOrEmpty, report.Status);
        }

        [Fact]
        public async Task GetAccountTransactionsReport_NoAccountWithGivenId_ReturnsFailResultWithAccountNotFoundStatus()
        {
            //Given
            var input = new GetTransactionsReportInput
            {
                ClientId = "03d2f6d8-5dc1-48c8-82fc-06330d2f4987",
                AccountResourceId = "03d2f6d8-5dc1-48c8-82fc-06330d2f4989"
            };
            //When
            var accountsRepoMock = new Mock<IAccountsRepo>();
            var transactionsRepoMock = new Mock<ITransactionsRepo>();
            var dateTimeProxyMock = new Mock<IDateTimeProxy>();

            var getTransactionsReport = new GetTransactionsReport(accountsRepoMock.Object, transactionsRepoMock.Object, dateTimeProxyMock.Object);
            var report = await getTransactionsReport.GetAccountTransactionsReport(input);
            //Then
            Assert.False(report.IsSuccess);
            Assert.Equal(GetTransactionsReportStatus.AccountNotFound, report.Status);
        }

        [Fact]
        public async Task GetAccountTransactionsReport_NoTransactionsForAccountInLastMonth_ReturnsFailResultWithTransactionsForLastMonthNotFoundStatus()
        {
            //Given
            var input = new GetTransactionsReportInput
            {
                ClientId = "03d2f6d8-5dc1-48c8-82fc-06330d2f4987",
                AccountResourceId = "5aed4c3e-fd57-4c45-8f75-95787e52ebcf"
            };
            //When
            var accountsRepoMock = new Mock<IAccountsRepo>();
            accountsRepoMock.Setup(a => a.GetAccounts(It.IsAny<string>()))
                            .ReturnsAsync(new List<Account> {
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
                                    Currency = "USD"
                                },
                            });
            var transactionsRepoMock = new Mock<ITransactionsRepo>();
            var dateTimeProxyMock = new Mock<IDateTimeProxy>();

            var getTransactionsReport = new GetTransactionsReport(accountsRepoMock.Object, transactionsRepoMock.Object, dateTimeProxyMock.Object);
            var report = await getTransactionsReport.GetAccountTransactionsReport(input);
            //Then
            Assert.False(report.IsSuccess);
            Assert.Equal(GetTransactionsReportStatus.TransactionsForLastMonthNotFound, report.Status);
        }


        [Theory]
        [InlineData(TransactionCategory.Food, 120)]
        [InlineData(TransactionCategory.Entertainment, 33.3)]
        [InlineData(TransactionCategory.Clothing, 110)]
        [InlineData(TransactionCategory.Travel, 20)]
        [InlineData(TransactionCategory.MedicalExpenses, 90)]
        public async Task GetAccountTransactionsReport_AccountHasTransactionsForLastMonth_ReturnsReportWithTransactionsFromLastMonth(TransactionCategory category, double sum)
        {
            //Given
            var input = new GetTransactionsReportInput
            {
                ClientId = "03d2f6d8-5dc1-48c8-82fc-06330d2f4987",
                AccountResourceId = "5aed4c3e-fd57-4c45-8f75-95787e52ebcf"
            };
            var currentDateTime = new DateTime(2021, 4, 11);
            //When
            var accountsRepoMock = new Mock<IAccountsRepo>();
            accountsRepoMock.Setup(a => a.GetAccounts(It.IsAny<string>()))
                            .ReturnsAsync(new List<Account> {
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
                                    Currency = "USD"
                                },
                            });

            /*
            Mock transactions for the following:
            - IBAN = NL69INGB0123456789
                - Food - 120
                - Entertainment - 33.3
                - Clothing - 110
                - Travel - 20
                - MedicalExpenses - 90
            */
            var transactionsRepoMock = new Mock<ITransactionsRepo>();
            transactionsRepoMock.Setup(t => t.GetTransactionsFromLastMonth(It.IsAny<string>()))
                                .ReturnsAsync(new List<Transaction>() {
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
                                        Amount = 110,
                                        CategoryId = 3,
                                        TransactionDate = new DateTime(2021, 3, 15)
                                    },
                                    new Transaction {
                                        Iban = "NL69INGB0123456789",
                                        TransactionId = 4,
                                        Amount = 33.3,
                                        CategoryId = 2,
                                        TransactionDate = new DateTime(2021, 3, 15)
                                    },
                                    new Transaction {
                                        Iban = "NL69INGB0123456789",
                                        TransactionId = 5,
                                        Amount = 90,
                                        CategoryId = 5,
                                        TransactionDate = new DateTime(2021, 3, 10)
                                    },
                                    new Transaction {
                                        Iban = "NL69INGB0123456789",
                                        TransactionId = 6,
                                        Amount = 20,
                                        CategoryId = 4,
                                        TransactionDate = new DateTime(2021, 3, 10)
                                    },
                                });

            var dateTimeProxyMock = new Mock<IDateTimeProxy>();
            dateTimeProxyMock.Setup(d => d.UtcNow())
                            .Returns(currentDateTime);

            var getTransactionsReport = new GetTransactionsReport(accountsRepoMock.Object, transactionsRepoMock.Object, dateTimeProxyMock.Object);
            var report = await getTransactionsReport.GetAccountTransactionsReport(input);
            //Then
            Assert.True(report.IsSuccess);
            Assert.Equal(GetTransactionsReportStatus.Success, report.Status);
            Assert.Equal(report.Value.First(t => t.CategoryName == category.ToString()).TotalAmount, sum);
        }
    }
}