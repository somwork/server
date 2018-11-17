using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskHouseApi.Model;
using System.Linq;

namespace TaskHouseUnitTests
{
    public class ReferencesControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        ReferencesController controller;

        public ReferencesControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new ReferencesController(unitOfWork);
        }

        //Test retrieve all in repository
        [Fact]
        public void ReferencesController_Get_ReturnAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Reference>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(3, resultObject.Count());
        }

        [Fact]
        //Test GET with id
        public void ReferencesController_Get_ReturnObject_WhenIdIsValid()
        {
            //Arrange id for reference object
            int referenceId = 1;

            //Act
            var result = controller.Get(referenceId);
            var resultAsObject = controller.Get(referenceId) as ObjectResult;
            var resultObject = resultAsObject.Value as Reference;

            //Assert - Checks if the returned object is the same type and then checks id
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(referenceId, resultObject.Id);
        }

        [Fact]
        // Test GET with invalid ID
        public void ReferencesController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            //Arrange id for location object
            int referenceId = 403;

            //Act
            var result = controller.Get(referenceId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        //Test POST for creating new reference
        public void ReferencesController_Create_ReturnsObject_WhenNewObject()
        {
            //Arrange new ObjectResult
            var reference = new Reference();
            reference.Rating = 2;

            //Act
            var result = controller.Create(reference);
            var resultAsObject = controller.Create(reference) as ObjectResult;
            var resultObject = resultAsObject.Value as Reference;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(reference.Rating, resultObject.Rating);
        }

        [Fact]
        //Test POST for creating new reference that is null
        public void ReferencesController_Create_ReturnsBadRequest_WhenObjectIsNull()
        {
            //Arrange new ObjectResult
            Reference reference = null;

            //Act
            var result = controller.Create(reference);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        // Test PUT for update reference
        public void ReferencesController_Update_ReturnsNoContentResultAndCreatedbject_WhenParametersAreValid()
        {
            //Arrange
            Reference reference = new Reference()
            {
                Id = 1,
                Rating = 4,
                Statement = "Good",
                WorkerId = 4,
                TaskId = 1
            };
            int id = 1;

            //Act
            var result = controller.Update(id, reference);
            var resultAsObject = controller.Get(reference.Id) as ObjectResult;
            var resultObject = resultAsObject.Value as Reference;

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(reference.Rating, resultObject.Rating);
        }

        ///Test put with invalid Id and null reference object
        [Fact]
        public void ReferencesController_Update_ReturnsBadRequestResult_WhenReferenceIsNull()
        {
            //Arrange
            Reference reference = null;
            int id = 0;

            //Act
            var result = controller.Update(id, reference);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        // Test PUT for update reference when Id is invalid
        public void ReferencesController_Update_ReturnsBadRequest_WhenIdIsInvalid()
        {
            //Arrange
            Reference reference = new Reference()
            {
                Id = 1,
                Rating = 4,
                Statement = "Good",
                WorkerId = 4,
                TaskId = 1
            };
            int id = 100;

            //Act
            var result = controller.Update(id, reference);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        // Test DELETE for Reference
        public void ReferencesController_Delete_ReturnsNoContentResult_WhenDeleted()
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
        //Test DELETE for invalid Id for Reference
        public void ReferencesController_Delete_ReturnsNotFoundResult_WhenIdInvalid()
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
