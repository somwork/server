using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TaskHouseUnitTests
{
    public class TasksControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        TasksController controller;

        public TasksControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new TasksController(unitOfWork);
        }

        ///Adds a httpContext to a TasksController
        ///and adds a User with two claims:
        ///1: NameIdentifier, the user Id
        ///2: Role, the user role/type
        private TasksController login(TasksController con)
        {
            con.ControllerContext = new ControllerContext();
            //Creates a new HttpContext
            con.ControllerContext.HttpContext = new DefaultHttpContext();

            //Adds a User with claim to the current context
            con.ControllerContext.HttpContext.User.AddIdentity(new ClaimsIdentity(new List<Claim>() {
                //Adds a claim for nameIdentifier, user Id
                new Claim(ClaimTypes.NameIdentifier, "1"),
                //Adds a claim for role, user role/tupe
                new Claim(ClaimTypes.Role, "TaskHouseApi.Model.Employer")
            }));

            //Returns the controller
            return con;
        }

        ///Test Get all
        [Fact]
        public void TasksController_Get_ReturnsAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Task>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(4, resultObject.Count());
        }

        ///Test Get with valid Id as parameter
        [Fact]
        public void TasksController_Get_ReturnsObjectReponseWithCorrectTask_WhenGivenValidId()
        {
            //arrange
            int taskId = 2;

            //Act
            var result = controller.Get(taskId);
            var resultAsObject = controller.Get(taskId) as ObjectResult;
            var resultObject = resultAsObject.Value as TaskHouseApi.Model.Task;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(taskId, resultObject.Id);
        }

        ///Test Get with invalid Id as parameter
        [Fact]
        public void TasksController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            //arrange
            int taskId = 5000;

            //Act
            var result = controller.Get(taskId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        /// Test Post with valid Task
        [Fact]
        public void TasksController_Create_ReturnsObjectResultContainingCreatedTask_WhenGivenValidTask()
        {
            //Arrange
            TaskHouseApi.Model.Task task = new TaskHouseApi.Model.Task();

            controller = login(controller);

            task.Description = "TestTask";

            //Act
            var result = controller.Create(task);
            var createdResultObject = result as ObjectResult;
            var createdTask = createdResultObject.Value as TaskHouseApi.Model.Task;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(task.Description, createdTask.Description);
        }

        ///Test post with null Task object
        [Fact]
        public void TasksController_Create_ReturnsBadRequest_WhenGivenNullTask()
        {
            //Arrange
            TaskHouseApi.Model.Task task = null;

            controller = login(controller);

            //Act
            var result = controller.Create(task);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            //Major inconsistencies in whether is return BadRequestResult or BadRequestObjectResult
        }

        ///Test put with valid Id and Task object
        [Fact]
        public void TasksController_Update_ReturnsNoContentResultAndCreatedObject_WhenParametersAreValid()
        {
            //Arrange
            TaskHouseApi.Model.Task task = new TaskHouseApi.Model.Task()
            {
                Id = 1,
                Description = "UpdatedTask"
            };
            int id = 1;

            //Act
            var result = controller.Update(task);
            var updatedResultObject = controller.Get(id) as ObjectResult;
            var updatedTask = updatedResultObject.Value as TaskHouseApi.Model.Task;

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(updatedTask.Description, task.Description);
        }

        ///Test put with valid Id and null Task object
        [Fact]
        public void TasksController_Update_ReturnsBadRequestResult_WhenTaskIsNull()
        {
            //Arrange
            TaskHouseApi.Model.Task task = null;

            //Act
            var result = controller.Update(task);

            //Assert
            Assert.IsType<BadRequestResult>(result);
            //inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        ///Test put with valid Id and Task object, on not existing Task
        [Fact]
        public void TasksController_Update_ReturnsNotFoundResult_WhenParametersAreValidButTaskDoesNotExist()
        {
            //Arrange
            TaskHouseApi.Model.Task task = new TaskHouseApi.Model.Task()
            {
                Id = 10,
                Description = "Task1"
            };

            //Act
            var result = controller.Update(task);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        ///Test Delete returns NoContentResult with valid Id
        [Fact]
        public void TasksController_Delete_ReturnsNoContentResult_WhenIdIsValid()
        {
            //Arrange
            int id = 1;

            //Act
            var result = controller.Delete(id);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        ///Test if delete actually deletes with valid Id
        [Fact]
        public void TasksController_Delete_ActuallyDeletes_WhenIdIsValid()
        {
            //Arrange
            int id = 1;

            //Act
            var result = controller.Delete(id);
            var getDeletedTaskResult = controller.Get(id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.IsType<NotFoundResult>(getDeletedTaskResult);
        }

        ///Test Delete with invalid Id
        [Fact]
        public void TasksController_Delete_ReturnsNotFoundResult_WhenIdIsInvalid()
        {
            //Arrange
            int id = 2500;

            //Act
            var result = controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
