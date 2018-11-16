using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaskHouseApi.Service;

namespace TaskHouseUnitTests
{
    public class WorkersControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        WorkersController controller;
        IPasswordService passwordService = new PasswordService();

        public WorkersControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new WorkersController(unitOfWork, passwordService);
        }

        [Fact]
        public void WorkerController_Get_ReturnsObjectResult_WhenGivenValidId()
        {
            int Id = 4;
            var result = controller.Get(Id);
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as Worker;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(Id, resultObject.Id);
        }

        [Fact]
        public void WorkerController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            int WorkerId = 5000;

            var result = controller.Get(WorkerId) as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void WorkerController_Get_ReturnsAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<User>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(3, resultObject.Count());
        }

        [Fact]
        public void WorkerController_Create_ReturnsObjectResult_withValidWorker()
        {
            Worker TestWorker = new Worker()
            {
                Id = 5,
                Username = "Tusername",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=",//1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen6",
                Salt = "upYKQSsrlub5JAID61/6pA=="
            };

            var result = controller.Create(TestWorker);
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as TaskHouseApi.Model.Worker;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(TestWorker.Id, resultObject.Id);
        }

        [Fact]
        public void WorkerController_Create_ReturnsBadRequest_WhenGivenNullWorker()
        {
            Worker worker = null;

            var result = controller.Create(worker);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void WorkerController_Delete_ReturnsNoContentResult_WhenIdIsValid()
        {
            int Id = 4;
            var result = controller.Delete(Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void WorkerController_Update_ReturnsBadRequestResult_WhenWorkerIsNull()
        {
            Worker worker = null;

            var result = controller.Update(0, worker);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void WorkerController_Delete_ActuallyDeletes_WhenIdIsValid()
        {
            var Id = 1;

            controller.Delete(Id);
            var result = controller.Get(Id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void WorkerController_Delete_ReturnsNotFoundResult_WhenIdIsInvalid()
        {
            int id = 2500;

            var result = controller.Delete(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void WorkerController_Update_ReturnsObjectResult_withValidIdAndValidWorker()
        {
            int Id = 4;
            Worker worker = new Worker()
            {
                Id = 4,
                Username = "Tusernamedasdasg",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen6",
                Salt = "upYKQSsrlub5JAID61/6pA=="
            };

            var Result = controller.Update(Id, worker);
            var resultAsObject = controller.Get(Id) as ObjectResult;
            var resultObject = resultAsObject.Value as Worker;

            Assert.IsType<NoContentResult>(Result);
            Assert.Equal(worker.Username, resultObject.Username);
        }
    }
}
