using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Api.Controllers;
using Accounts.Api.Features.Transactions.Report;
using Accounts.Api.Features.Transactions.Report.Models;
using Accounts.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Accounts.Api.UnitTests.Controllers
{
    public class TransactionsControllerTests
    {
        [Fact]
        public async Task Report_ResultStatusIsInputNull_ReturnsBadRequest()
        {
            //Given
            var input = new GetTransactionsReportInput();
            //When
            var getTransactionsReportMock = new Mock<IGetTransactionsReport>();
            getTransactionsReportMock.Setup(r => r.GetAccountTransactionsReport(It.IsAny<GetTransactionsReportInput>()))
                                    .ReturnsAsync(Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Fail(GetTransactionsReportStatus.InputNull, String.Empty));

            var controller = new TransactionsController(getTransactionsReportMock.Object);
            var result = await controller.Report(input);
            //Then
            var actionResult = Assert.IsType<ActionResult<List<TransactionsPerCategoryAggregationModel>>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Report_ResultStatusIsClientIdNullOrEmpty_ReturnsBadRequest()
        {
            //Given
            var input = new GetTransactionsReportInput();
            //When
            var getTransactionsReportMock = new Mock<IGetTransactionsReport>();
            getTransactionsReportMock.Setup(r => r.GetAccountTransactionsReport(It.IsAny<GetTransactionsReportInput>()))
                                    .ReturnsAsync(Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Fail(GetTransactionsReportStatus.ClientIdNullOrEmpty, String.Empty));

            var controller = new TransactionsController(getTransactionsReportMock.Object);
            var result = await controller.Report(input);
            //Then
            var actionResult = Assert.IsType<ActionResult<List<TransactionsPerCategoryAggregationModel>>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Report_ResultStatusIsAccountResourceIdNullOrEmpty_ReturnsBadRequest()
        {
            //Given
            var input = new GetTransactionsReportInput();
            //When
            var getTransactionsReportMock = new Mock<IGetTransactionsReport>();
            getTransactionsReportMock.Setup(r => r.GetAccountTransactionsReport(It.IsAny<GetTransactionsReportInput>()))
                                    .ReturnsAsync(Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Fail(GetTransactionsReportStatus.AccountResourceIdNullOrEmpty, String.Empty));

            var controller = new TransactionsController(getTransactionsReportMock.Object);
            var result = await controller.Report(input);
            //Then
            var actionResult = Assert.IsType<ActionResult<List<TransactionsPerCategoryAggregationModel>>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }


        [Fact]
        public async Task Report_ResultStatusIsAccountNotFound_ReturnsNotFound()
        {
            //Given
            var input = new GetTransactionsReportInput();
            //When
            var getTransactionsReportMock = new Mock<IGetTransactionsReport>();
            getTransactionsReportMock.Setup(r => r.GetAccountTransactionsReport(It.IsAny<GetTransactionsReportInput>()))
                                    .ReturnsAsync(Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Fail(GetTransactionsReportStatus.AccountNotFound, String.Empty));

            var controller = new TransactionsController(getTransactionsReportMock.Object);
            var result = await controller.Report(input);
            //Then
            var actionResult = Assert.IsType<ActionResult<List<TransactionsPerCategoryAggregationModel>>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Report_ResultStatusIsTransactionsForLastMonthNotFound_ReturnsNotFound()
        {
            //Given
            var input = new GetTransactionsReportInput();
            //When
            var getTransactionsReportMock = new Mock<IGetTransactionsReport>();
            getTransactionsReportMock.Setup(r => r.GetAccountTransactionsReport(It.IsAny<GetTransactionsReportInput>()))
                                    .ReturnsAsync(Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Fail(GetTransactionsReportStatus.TransactionsForLastMonthNotFound, String.Empty));

            var controller = new TransactionsController(getTransactionsReportMock.Object);
            var result = await controller.Report(input);
            //Then
            var actionResult = Assert.IsType<ActionResult<List<TransactionsPerCategoryAggregationModel>>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task Report_ResultIsSuccess_ReturnsSuccess()
        {
            //Given
            var input = new GetTransactionsReportInput();
            //When
            var getTransactionsReportMock = new Mock<IGetTransactionsReport>();
            getTransactionsReportMock.Setup(r => r.GetAccountTransactionsReport(It.IsAny<GetTransactionsReportInput>()))
                                    .ReturnsAsync(Result<GetTransactionsReportStatus, IEnumerable<TransactionsPerCategoryAggregationModel>>.Success(GetTransactionsReportStatus.AccountNotFound, new List<TransactionsPerCategoryAggregationModel>()));

            var controller = new TransactionsController(getTransactionsReportMock.Object);
            var result = await controller.Report(input);
            //Then
            var actionResult = Assert.IsType<ActionResult<List<TransactionsPerCategoryAggregationModel>>>(result);
            Assert.IsType<List<TransactionsPerCategoryAggregationModel>>(actionResult.Value);
        }
    }
}