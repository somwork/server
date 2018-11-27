using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskHouseUnitTests.FakeRepositories;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseUnitTests.UnitTests
{
    public class CurrenciesControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        CurrenciesController controller;
        CurrencyRESTService service = new CurrencyRESTService();

        public CurrenciesControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new CurrenciesController(unitOfWork);
        }

        //TODO write tests!
        // Test retrive
        [Fact]
        public async void CurrenciesController_Get_ReturnsObjectResult_WhenRequestIsOK()
        {
            var result = await controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Currency>;

            Assert.IsType<ObjectResult>(result);
        }
    }
}
