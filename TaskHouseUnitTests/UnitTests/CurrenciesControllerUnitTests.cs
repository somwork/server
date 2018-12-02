using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskHouseUnitTests.FakeRepositories;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Internal;
using System.Linq;

namespace TaskHouseUnitTests.UnitTests
{
    public class CurrenciesControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        CurrenciesController controller;
        ICurrencyRESTService service;

        public CurrenciesControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            service = new FakeCurrencyRESTService();
            controller = new CurrenciesController(unitOfWork, service);
        }

        private CurrenciesController createContext(CurrenciesController con)
        {
            con.ControllerContext = new ControllerContext();
            //Creates a new HttpContext
            con.ControllerContext.HttpContext = new DefaultHttpContext();

            con.ObjectValidator = new DefaultObjectValidator
            (
                new DefaultModelMetadataProvider
                (
                    new DefaultCompositeMetadataDetailsProvider(Enumerable.Empty<IMetadataDetailsProvider>())
                ),
                new List<Microsoft.AspNetCore.Mvc.ModelBinding.Validation.IModelValidatorProvider>()
            );

            //Returns the controller
            return con;
        }

        // Test retrive
        [Fact]
        public async void CurrenciesController_Get_ReturnsObjectResult_WhenRequestIsOK()
        {
            var controllereee = controller;
            var result = await controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Currency>;

            Assert.IsType<ObjectResult>(result);
        }
    }
}
