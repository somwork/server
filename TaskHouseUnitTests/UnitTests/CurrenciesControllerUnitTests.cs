using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Model;
using System.Net.Http;

namespace TaskHouseUnitTests.UnitTests
{
    public class CurrenciesControllerUnitTests
    {
        CurrenciesController controller;
        CurrencyRESTService service = new CurrencyRESTService();

        public CurrenciesControllerUnitTests()
        {
        }

        //TODO write tests!
        [Fact]
        public void CurrenciesController_Get_ReturnsObjectResult_WhenRequestIsOK()
        {
            var result = 
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as Currency;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(Id, resultObject.Id);
        }
    }
}
