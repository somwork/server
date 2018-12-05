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
using TaskHouseUnitTests.FakeRepositories;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json;

namespace TaskHouseUnitTests.UnitTests
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
        private TasksController createContext(TasksController con, String userType)
        {
            con.ControllerContext = new ControllerContext();
            //Creates a new HttpContext
            con.ControllerContext.HttpContext = new DefaultHttpContext();

            //Adds a User with claim to the current context
            con.ControllerContext.HttpContext.User.AddIdentity(new ClaimsIdentity(new List<Claim>() {
                //Adds a claim for nameIdentifier, user Id
                new Claim(ClaimTypes.NameIdentifier, "1"),
                //Adds a claim for role, user role/tupe
                new Claim(ClaimTypes.Role, "TaskHouseApi.Model." + userType)
            }));


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
            task.UrgencyString = "norush";

            controller = createContext(controller, "Employer");

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

            controller = createContext(controller, "Employer");

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
            var result = controller.Update(id, task);
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
            int id = 0;

            //Act
            var result = controller.Update(id, task);

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
            int id = 10;

            //Act
            var result = controller.Update(id, task);

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

        ///Test creation of estimate through Task controller
        ///All data valid
        [Fact]
        public void TasksController_Estimate_Create_ReturnsObjectResultWithCorrectObject_WhenIdAndEstimateAreValid()
        {
            //Arrange
            controller = createContext(controller, "worker");
            Estimate e = new Estimate()
            {
                Id = 5,
                TotalHours = 10,
                Complexity = 1,
                HourlyWage = 110,
                Urgency = 1,
                TaskId = 1,
            };

            int taskId = 1;

            //Act
            var result = controller.CreateEstimate(taskId, e);
            var objectResult = result as ObjectResult;
            var createdEstimate = objectResult.Value as Estimate;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(createdEstimate.Id, e.Id);
            Assert.Equal(createdEstimate.TaskId, e.TaskId);
        }

        ///Test creation of estimate through Task controller
        ///Invalid task id
        [Fact]
        public void TasksController_Estimate_Create_ReturnsBadRequest_WhenGivenInvalidTaskIdAndValidEstimate()
        {
            //Arrange
            controller = createContext(controller, "worker");
            Estimate e = new Estimate()
            {
                Id = 5,
                TotalHours = 10,
                Complexity = 1,
                HourlyWage = 110,
                Urgency = 1,
            };

            int taskId = 500;

            //Act
            var result = controller.CreateEstimate(taskId, e);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        ///Test creation of estimate through Task controller
        ///Null estimate
        [Fact]
        public void TasksController_Estimate_Create_ReturnsBadRequest_WhenGivenNullEsitmate()
        {
            //Arrange
            controller = createContext(controller, "Worker");
            Estimate e = null;

            int taskId = 1;

            //Act
            var result = controller.CreateEstimate(taskId, e);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        ///Test creation of estimate through Task controller
        ///Invalid task id
        [Fact]
        public void TasksController_Estimate_Create_AverageEstimateForTaskIszero_WhenEstimateListIsEmpty()
        {
            //arrange
            int taskId = 1;

            //Act
            var result = controller.Get(taskId);
            var resultAsObject = result as ObjectResult;
            var resultObject = resultAsObject.Value as TaskHouseApi.Model.Task;

            //Assert
            Assert.Equal(0, resultObject.AverageEstimate);
        }

        ///Test CompleteTask with existing, uncompleted, task
        [Fact]
        public void TasksController_Update_CompleteTask_SetsCompletedToTrue_WithValidTask()
        {
            //Arrange
            int taskId = 1;
            controller = createContext(controller, "Employer");

            //Act
            var result = controller.CompleteTask(taskId);
            var resultAsObject = result as ObjectResult;
            var resultObject = resultAsObject.Value as TaskHouseApi.Model.Task;

            //Assert
            Assert.Equal(resultObject.Completed, true);
        }

        ///Test CompleteTask when task doesn't exist
        [Fact]
        public void TasksController_Update_CompleteTask_RetunsBadRequest_WithInvalidTask()
        {
            //Arrange
            int taskId = 100;
            controller = createContext(controller, "Employer");

            //Act
            var result = controller.CompleteTask(taskId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        ///Test completing test, when task is already completed
        [Fact]
        public void TasksController_Update_CompleteTask_RetunsBadRequest_WhenTaskIsAlreadyCompleted()
        {
            //Arrange
            int taskId = 1;
            controller = createContext(controller, "Employer");
            var retrieveTask = controller.Get(taskId) as ObjectResult;
            var task = retrieveTask.Value as Task;
            task.Completed = true;

            //Act
            var result = controller.CompleteTask(taskId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
