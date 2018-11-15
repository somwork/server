using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TaskHouseUnitTests
{
    public class LocationsControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        LocationsController controller;

        public LocationsControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new LocationsController(unitOfWork);
        }

        //Test retrieve all in repository
        [Fact]
        public void LocationsController_Get_ReturnAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Location>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(3, resultObject.Count());
        }

        [Fact]
        //Test GET with id
        public void LocationsController_Get_ReturnObject_WhenIdIsValid()
        {
            //Arrange id for location object
            int locationId = 1;

            //Act
            var result = controller.Get(locationId);
            var resultAsObject = controller.Get(locationId) as ObjectResult;
            var resultObject = resultAsObject.Value as Location;

            //Assert - Checks if the returned object is the same type and then checks id
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(locationId, resultObject.Id);
        }

        [Fact]
        // Test GET with invalid ID
        public void LocationController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            //Arrange id for location object
            int locationId = 403;

            //Act
            var result = controller.Get(locationId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        //Test POST for creating new location
        public void LocationController_Create_ReturnsObject_WhenNewObject()
        {
            //Arrange new ObjectResult
            var location = new Location();
            location.Country = "Contry100";

            //Act
            var result = controller.Create(location);
            var resultAsObject = controller.Create(location) as ObjectResult;
            var resultObject = resultAsObject.Value as Location;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(location.Country, resultObject.Country);
        }

        [Fact]
        //Test POST for creating new location that is null
        public void LocationController_Create_ReturnsBadRequest_WhenObjectIsNull()
        {
            //Arrange new ObjectResult
            Location location = null;

            //Act
            var result = controller.Create(location);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        // Test PUT for update Location
        public void LocationController_Update_ReturnsNoContentResultAndCreatedbject_WhenParametersAreValid()
        {
            //Arrange
            Location location = new Location()
            {
                Id = 1,
                Country = "UpdatedCountry"
            };

            //Act
            var result = controller.Update(location);
            var resultAsObject = controller.Get(location.Id) as ObjectResult;
            var resultObject = resultAsObject.Value as Location;

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(location.Country, resultObject.Country);
        }

        ///Test put with invalid Id and null Location object
        [Fact]
        public void LocationController_Update_ReturnsBadRequestResult_WhenLocationIsNull()
        {
            //Arrange
            Location location = null;

            //Act
            var result = controller.Update(location);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        // Test PUT for update Location when Id is invalid
        public void LocationController_Update_ReturnsBadRequest_WhenIdIsInvalid()
        {
            //Arrange
            Location location = new Location()
            {
                Id = 100,
                Country = "UpdatedCountry"
            };

            //Act
            var result = controller.Update(location);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        // Test DELETE for Location
        public void LocationController_Delete_ReturnsNoContentResult_WhenDeleted()
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
        //Test DELETE for invalid Id for Location
        public void LocationController_Delete_ReturnsNotFoundResult_WhenIdInvalid()
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
