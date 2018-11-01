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
        ITasksController controller;
        ITaskRepository repo;

        public TasksControllerUnitTests() 
        { 
            repo = new FakeTaskRepository();
            controller = new TasksControllerUnitTests(repo);
        }

         ///Test Get all
        [Fact]
        public async void  TasksController_ReturnsAllElementsInRepo() 
        { 
            //Arrange and act
            IEnumerable<TaskHouseApi.Model.Task> result = await controller.Get();

            //Asserts
            Assert.Equal(3, result.Count());

        }

        ///Test Get with valid Id as parameter
        [Fact]
        public async void TasksController_Get_ReturnsObjectReponse_WhenGivenValidId() 
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

        /// Test Post with valid Skill 
        [Fact]
        public async void TasksController_Create_ReturnsObjectResult_WhenGivenValidSkill()
        { 
            //Arrange
            TaskHouseApi.Model.Task task = new TaskHouseApi.Model.Task();
            task.Description = "TestTask";

            //Act
            var result = await controller.Create(task);
            var returnedResultObject = await controller.Get(id) as ObjectResult;
            var returnedTask = updatedResultObject.Value as TaskHouseApi.Model.Task;

            //Assert
            var assertResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(task.Description, returnedTask.Description);

        }

        ///Test post with null Skill object
        [Fact]
         public async void TasksController_Create_ReturnsBadRequest_WhenGivenNullSkill()
        { 
            //Arrange
            Skill skill = null;

            //Act
            var result = await controller.Create(skill);

            //Assert
            var assertResult = Assert.IsType<BadRequestObjectResult>(result);
            //Major inconsistencies in whether is return BadRequestResult or BadRequestObjectResult

        }

        ///Test put with valid Id and Skill object
        [Fact]
        public async void TasksController_Update_ReturnsNoContentResult_WhenParametersAreValid()
        { 
            //Arrange
            Skill skill = new Skill(){
                Id = 1, 
                Title = "UpdatedSkill"
            };
            int id = 1;

            //Act
            var result = await controller.Update(id, skill);
            var updatedResultObject = await controller.Get(id) as ObjectResult;
            var updatedSkill = updatedResultObject.Value as Skill;

            //Assert
            var assertResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(updatedSkill.Title, "UpdatedSkill");
        }

        ///Test put with invalid Id and valid Skill object
        [Fact]
        public async void TasksController_Update_ReturnsBadRequestResult_WhenIdIsInvalid()
        { 
            //Arrange
            Skill skill = new Skill(){
                Id = 1, 
                Title = "Skill1"
            };
            int id = 50;

            //Act
            var result = await controller.Update(id, skill);

            //Assert
            var assertResult = Assert.IsType<BadRequestResult>(result); 
            //inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        ///Test put with valid Id and null Skill object
        [Fact]
        public async void TasksController_Update_ReturnsBadRequestResult_WhenSkillIsNull()
        { 
            //Arrange
            Skill skill = null;
            int id = 1;

            //Act
            var result = await controller.Update(id, skill);

            //Assert
            var assertResult = Assert.IsType<BadRequestResult>(result); 
            //inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        ///Test put with invalid Id and null Skill object
        [Fact]
        public async void TasksController_Update_ReturnsBadRequestResult_WhenIdIsInvalidAndSkillIsNull()
        { 
            //Arrange
            Skill skill = null;
            int id = 2600;

            //Act
            var result = await controller.Update(id, skill);

            //Assert
            var assertResult = Assert.IsType<BadRequestResult>(result); 
            //Inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        ///Test put with valid Id and Skill object, on not existing skill
        [Fact]
        public async void TasksController_Update_ReturnsNotFoundResult_WhenParametersAreValidButSkillDoesNotExist()
        { 
            //Arrange
            Skill skill = new Skill(){
                Id = 10, 
                Title = "Skill1"
            };
            int id = 10;

            //Act
            var result = await controller.Update(id, skill);

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
            var getDeletedSkillResult = await controller.Get(id);

            //Assert
            var assertResult = Assert.IsType<NoContentResult>(result);
            var assertDeleteResult = Assert.IsType<NotFoundResult>(getDeletedSkillResult);
            
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
            var asserrResult = Assert.IsType<NotFoundResult>(result);
        }

    }
}