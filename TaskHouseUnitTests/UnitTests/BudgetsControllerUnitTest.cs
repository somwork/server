using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskHouseApi.Model;
using System.Linq;
using System;
using TaskHouseUnitTests.FakeRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace TaskHouseUnitTests.UnitTests
{
    public class BudgetsControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        BudgetsController controller;

        public BudgetsControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new BudgetsController(unitOfWork);
        }

        private BudgetsController createContext(BudgetsController con)
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

        //Test retrieve all in repository
        [Fact]
        public void BudgetsController_Get_ReturnAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Budget>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(3, resultObject.Count());
        }

        [Fact]
        //Test GET with id
        public void BudgetsController_Get_ReturnObject_WhenIdIsValid()
        {
            //Arrange id for budget object
            int budgetId = 1;

            //Act
            var result = controller.Get(budgetId);
            var resultAsObject = controller.Get(budgetId) as ObjectResult;
            var resultObject = resultAsObject.Value as Budget;

            //Assert - Checks if the returned object is the same type and then checks id
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(budgetId, resultObject.Id);
        }

        [Fact]
        // Test GET with invalid ID
        public void BudgetsController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            //Arrange id for location object
            int budgetId = 403;

            //Act
            var result = controller.Get(budgetId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        //Test POST for creating new budget
        public void BudgetsController_Create_ReturnsObject_WhenNewObject()
        {
            controller = createContext(controller);
            //Arrange new ObjectResult
            var budget = new Budget();
            budget.From = 500;

            //Act
            var result = controller.Create(budget);
            var resultAsObject = result as ObjectResult;
            var resultObject = resultAsObject.Value as Budget;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(budget.From, resultObject.From);
        }

        [Fact]
        //Test POST for creating new budget that is null
        public void BudgetsController_Create_ReturnsBadRequest_WhenObjectIsNull()
        {
            //Arrange new ObjectResult
            Budget budget = null;

            //Act
            var result = controller.Create(budget);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        // Test PUT for update budget
        public void BudgetsController_Update_ReturnsNoContentResultAndCreatedbject_WhenParametersAreValid()
        {
            //Arrange
            Budget budget = new Budget()
            {
                Id = 1,
                From = 0,
                To = 100,
                Currency = "EUR",
            };
            int id = 1;

            //Act
            var result = controller.Update(id, budget);
            var resultAsObject = controller.Get(budget.Id) as ObjectResult;
            var resultObject = resultAsObject.Value as Budget;

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(budget.From, resultObject.From);
        }

        ///Test put with invalid Id and null budget object
        [Fact]
        public void BudgetsController_Update_ReturnsBadRequestResult_WhenbudgetIsNull()
        {
            //Arrange
            Budget budget = null;
            int id = 0;

            //Act
            var result = controller.Update(id, budget);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        // Test PUT for update budget when Id is invalid
        public void BudgetsController_Update_ReturnsBadRequest_WhenIdIsInvalid()
        {
            //Arrange
            Budget budget = new Budget()
            {
                Id = 1,
                From = 0,
                To = 100,
                Currency = "EUR",
            };
            int id = 100;

            //Act
            var result = controller.Update(id, budget);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        // Test DELETE for budget
        public void BudgetsController_Delete_ReturnsNoContentResult_WhenDeleted()
        {
            //Arrange
            int Id = 1;

            //Act
            var result = controller.Delete(Id);
            var getDeletedResult = controller.Get(Id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.IsType<NotFoundResult>(getDeletedResult);
        }

        [Fact]
        //Test DELETE for invalid Id for budget
        public void BudgetsController_Delete_ReturnsNotFoundResult_WhenIdInvalid()
        {
            //Arrange
            int Id = 100;

            //Act
            var result = controller.Delete(Id);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }
    }
}
