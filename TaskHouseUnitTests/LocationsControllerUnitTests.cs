using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TaskHouseUnitTests
{
    public class LocationsControllerUnitTests
    {
        ILocationRepository repo;
        LocationsController controller;

        public LocationsControllerUnitTests()
        {
            repo = new FakeLocationRepository();
            controller = new LocationsController(repo);
        }

        //Test retrieve all in repository
        [Fact]
        public async void LocationsController_Get_ReturnAllElementsInRepo_WhenGivenNoParameters()
        {
            //Arrange object
            IEnumerable<Location> resultset;

            //Act
            resultset = await controller.Get();

            //Assert - Checks the size of the list matches with 3 which is the size
            Assert.Equal(3,resultset.Count());
        }

        [Fact]
        //Test GET with id
        public async void LocationsController_Get_ReturnObject_WhenIdIsValid()
        {
            //Arrange id for location object
            int locationId = 1;

            //Act
            var result = await controller.Get(locationId);
            var resultAsObject = await controller.Get(locationId) as ObjectResult;
            var resultObject = resultAsObject.Value as Location;

            //Assert - Checks if the returned object is the same type and then checks id
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(locationId, resultObject.Id);
        }

        [Fact]
        // Test GET with invalid ID
        public  async void LocationController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            //Arrange id for location object
            int locationId = 403;

            //Act
            var result = await controller.Get(locationId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        //Test POST for creating new location
        public async void LocationController_Create_ReturnsObject_WhenNewObject()
        {
            //Arrange new ObjectResult
            var location = new Location();
            location.Country = "Contry100";

            //Act
            var result = await controller.Create(location);
            var resultAsObject = await controller.Create(location) as ObjectResult;
            var resultObject = resultAsObject.Value as Location;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(location.Country,resultObject.Country);
        }

        [Fact]
        //Test POST for creating new location that is null
        public async void LocationController_Create_ReturnsBadRequest_WhenObjectIsNull()
        {
            //Arrange new ObjectResult
            Location location = null;

            //Act
            var result = await controller.Create(location);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        // Test PUT for update Location
        public async void LocationController_Update_ReturnsNoContentResultAndCreatedbject_WhenParametersAreValid()
        {
            //Arrange
            Location location = new Location()
            {
                Id = 1,
                Country = "UpdatedCountry"
            };

            //Act
            var result = await controller.Update(location.Id,location);
            var resultAsObject = await controller.Get(location.Id) as ObjectResult;
            var resultObject = resultAsObject.Value as Location;

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(location.Country, resultObject.Country);
        }

        ///Test put with invalid Id and null Location object
        [Fact]
        public async void LocationController_Update_ReturnsBadRequestResult_WhenIdIsInvalidAndLocationIsNull()
        {
            //Arrange
            Location location = null;
            int Id = 100;

            //Act
            var result = await controller.Update(Id, location);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        // Test PUT for update Location when Id is invalid
        public async void LocationController_Update_ReturnsBadRequest_WhenIdIsInvalid()
        {
            //Arrange
            Location location = new Location()
            {
                Id = 100,
                Country = "UpdatedCountry"
            };

            //Act
            var result = await controller.Update(location.Id,location);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        // Test DELETE for Location
        public async void LocationController_Delete_ReturnsNoContentResult_WhenDeleted()
        {
            //Arrange
            int Id = 1;

            //Act
            var result = await controller.Delete(Id);
            var getDeletedResult = await controller.Get(Id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.IsType<NotFoundResult>(getDeletedResult);
        }

        [Fact]
        //Test DELETE for invalid Id for Location
        public async void LocationController_Delete_ReturnsNotFoundResult_WhenIdInvalid()
        {
            //Arrange
            int Id = 100;

            //Act
            var result = await controller.Delete(Id);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

    }
}
