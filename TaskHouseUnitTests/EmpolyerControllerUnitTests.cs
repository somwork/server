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
    public class EmpolyerControllerUnitTests
    {
        EmpolyerController controller;
        IEmpolyerRepository repo;

        public EmpolyerControllerUnitTests()
        {
            repo = new FakeEmpolyerRepository();
            controller = new EmpolyerController(repo);
        }
        
        /// Test Get all
        [Fact]
        public async void EmpolyerController_Get_ReturnsAllElementsInRepo_WhenGivenNoParameters()
        {

            IEnumerable<TaskHouse.api.Model.Empolyer> result = await controller.Get();


            Assert.Equal(3, result.Count());
            
        }

        ///Test Get with valid Id as parameter
        [Fact]
        public async void EmpolyerController_Get_ReturnsObjectReponseWithCorrectTask_WhenGivenValidId()
        {
            //arrange
            int empolyerId = 2;

            //Act
            var result = await controller.Get(empolyerId);
            var resultAsObject = await controller.Get(empolyerId) as ObjectResult;
            var resultObject = resultAsObject.Value as TaskHouseApi.Model.Task;

            //Assert
            var assertResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(empolyerId, resultObject.Id);

        }

        public async void EmpoyerController_Create_ReturnsObjekctResultContainningCreatedEmpolyer_WhenGivenVaildTask()
        {
            //Arrange
            TaskHouseApi.Model.Employer empolyer = new TaskHouseApi.Model.Task();
            empolyer.Description = "TestTask";

            //Act
            var result = await controller.Create(empolyer);
            var createdResultObject = result as ObjectResult;
            var createdTask = createdResultObject.Value as TaskHouseApi.Model.Task;

            //Assert
            var assertResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(empolyer.Description, createdTask.Description);

        }
        /// Test put with vaild Id and Empolyer object 
        [Fact]
        public async void TasksController_Update_ReturnsNoContentResultAndCreatedObject_WhenParametersAreValid()
        {
            //Arrange
            TaskHouseApi.Model.Task empolyer = new TaskHouseApi.Model.Task()
            {
                Id = 1,
                Description = "UpdatedEmpolyer"
            };
            int id = 1;

            //Act
            var result = await controller.Update(id, empolyer);
            var updatedResultObject = await controller.Get(id) as ObjectResult;
            var updatedEmpolyer = updatedResultObject.Value as TaskHouseApi.Model.Empolyer;

            //Assert
            var assertResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(updatedEmpolyer.Description, empolyer.Description);
        }

        

    }
}
