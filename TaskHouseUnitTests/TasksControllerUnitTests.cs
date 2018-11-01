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
    public class TasksControllerUnitTests
    { 
        TasksController controller;
        ITaskRepository repo;

        public TasksControllerUnitTests() 
        { 
            repo = new FakeTaskRepository();
            controller = new TasksController(repo);
        }

         ///Test Get all
        [Fact]
        public async void  TasksController_Get_ReturnsAllElementsInRepo_WhenGivenNoParameters() 
        { 
            //Arrange and act
            IEnumerable<TaskHouseApi.Model.Task> result = await controller.Get();

            //Asserts
            Assert.Equal(4, result.Count());

        }

        ///Test Get with valid Id as parameter
        [Fact]
        public async void TasksController_Get_ReturnsObjectReponseWithCorrectTask_WhenGivenValidId() 
        { 
            //arrange
            int taskId = 2;

            //Act
            var result = await controller.Get(taskId);
            var resultAsObject = await controller.Get(taskId) as ObjectResult;
            var resultObject = resultAsObject.Value as TaskHouseApi.Model.Task;

            //Assert
            var assertResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(taskId, resultObject.Id);

        }

        ///Test Get with invalid Id as parameter
        [Fact]
        public  async void TasksController_Get_ReturnsNotFound_WhenGivenInvalidId()
        { 
            //arrange
            int taskId = 5000;

            //Act
            var result = await controller.Get(taskId) as NotFoundResult;
            
            //Assert
            Assert.Equal(404, result.StatusCode);

        }

        /// Test Post with valid Task 
        [Fact]
        public async void TasksController_Create_ReturnsObjectResultContainingCreatedTask_WhenGivenValidSkill()
        { 
            //Arrange
            TaskHouseApi.Model.Task task = new TaskHouseApi.Model.Task();
            task.Description = "TestTask";

            //Act
            var result = await controller.Create(task);
            var createdResultObject = result as ObjectResult;
            var createdTask = createdResultObject.Value as TaskHouseApi.Model.Task;

            //Assert
            var assertResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(task.Description, createdTask.Description);

        }

        ///Test post with null Task object
        [Fact]
         public async void TasksController_Create_ReturnsBadRequest_WhenGivenNullSkill()
        { 
            //Arrange
            TaskHouseApi.Model.Task task = null;

            //Act
            var result = await controller.Create(task);

            //Assert
            var assertResult = Assert.IsType<BadRequestObjectResult>(result);
            //Major inconsistencies in whether is return BadRequestResult or BadRequestObjectResult

        }

        ///Test put with valid Id and Task object
        [Fact]
        public async void TasksController_Update_ReturnsNoContentResultAndCreatedbject_WhenParametersAreValid()
        { 
            //Arrange
            TaskHouseApi.Model.Task task = new TaskHouseApi.Model.Task(){
                Id = 1, 
                Description = "UpdatedTask"
            };
            int id = 1;

            //Act
            var result = await controller.Update(id, task);
            var updatedResultObject = await controller.Get(id) as ObjectResult;
            var updatedTask = updatedResultObject.Value as TaskHouseApi.Model.Task;

            //Assert
            var assertResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(updatedTask.Description, task.Description);
        }

        ///Test put with invalid Id and valid Task object
        [Fact]
        public async void TasksController_Update_ReturnsBadRequestResult_WhenIdIsInvalid()
        { 
            //Arrange
            TaskHouseApi.Model.Task task = new TaskHouseApi.Model.Task(){
                Id = 1, 
                Description = "Task1"
            };
            int id = 50;

            //Act
            var result = await controller.Update(id, task);

            //Assert
            var assertResult = Assert.IsType<BadRequestResult>(result); 
            //inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        ///Test put with valid Id and null Task object
        [Fact]
        public async void TasksController_Update_ReturnsBadRequestResult_WhenSkillIsNull()
        { 
            //Arrange
            TaskHouseApi.Model.Task task = null;
            int id = 1;

            //Act
            var result = await controller.Update(id, task);

            //Assert
            var assertResult = Assert.IsType<BadRequestResult>(result); 
            //inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        ///Test put with invalid Id and null Task object
        [Fact]
        public async void TasksController_Update_ReturnsBadRequestResult_WhenIdIsInvalidAndSkillIsNull()
        { 
            //Arrange
            TaskHouseApi.Model.Task task = null;
            int id = 2600;

            //Act
            var result = await controller.Update(id, task);

            //Assert
            var assertResult = Assert.IsType<BadRequestResult>(result); 
            //Inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        ///Test put with valid Id and Task object, on not existing Task
        [Fact]
        public async void TasksController_Update_ReturnsNotFoundResult_WhenParametersAreValidButSkillDoesNotExist()
        { 
            //Arrange
            TaskHouseApi.Model.Task task = new TaskHouseApi.Model.Task(){
                Id = 10, 
                Description = "Task1"
            };
            int id = 10;

            //Act
            var result = await controller.Update(id, task);

            //Assert
            var assertResult = Assert.IsType<NotFoundResult>(result);
        }


        ///Test Delete returns NoContentResult with valid Id
        [Fact] 
        public async void TasksController_Delete_ReturnsNoContentResult_WhenIdIsValid()
        { 
            //Arrange
            int id = 1; 

            //Act
            var result = await controller.Delete(id);

            //Assert
            var assertResult = Assert.IsType<NoContentResult>(result);
        }

         ///Test if delete actually deletes with valid Id
        [Fact] 
        public async void TasksController_Delete_ActuallyDeletes_WhenIdIsValid()
        { 
            //Arrange
            int id = 1; 

            //Act
            var result = await controller.Delete(id);
            var getDeletedTaskResult = await controller.Get(id);

            //Assert
            var assertResult = Assert.IsType<NoContentResult>(result);
            var assertDeleteResult = Assert.IsType<NotFoundResult>(getDeletedTaskResult);
            
        }

        ///Test Delete with invalid Id
        [Fact] 
        public async void TasksController_Delete_ReturnsNotFoundResult_WhenIdIsInvalid()
        { 
            //Arrange
            int id = 2500; 

            //Act
            var result = await controller.Delete(id);

            //Assert
            var assertResult = Assert.IsType<NotFoundResult>(result);
        }

    }
}