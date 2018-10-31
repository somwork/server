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
        public async void LocationsController_ReturnAllElementsInRepo()
        {

          //Arrange object
          IEnumerable<Location> resultset;

          //Act
          resultset = await controller.Get();

          //Assert - Checks the size of the list matches with 3 which is the size
          Assert.Equal(3,resultset.Count());
        }

        [Fact]
        //Test get with id
        public async void LocationsController_ReturnObject_WhenIdIsValid()
        {
          //Arrange id for location object
          int locationId = 1;

          //Fact
          var result = await controller.Get(locationId);
          var resultAsObject = await controller.Get(locationId) as ObjectResult;
          var resultObject = resultAsObject.Value as Location;

          //Assert - Checks if the returned object is the same type and then checks id
          var assertResult = Assert.IsType<ObjectResult>(result);
          Assert.Equal(locationId, resultObject.Id);
        }
    }
}
