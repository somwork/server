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
    public class WorkersControllerUnitTests
    {
        WorkersController controller;
        IWorkerRepository repo;

        public WorkersControllerUnitTests()
        {
            repo = new FakeWorkerRepository();
            controller = new WorkersController(repo);
        }

        [Fact]
        public async void WorkerController_Get_ReturnsObjectReponseWithCorrectEmpolyer_WhenGivenValidId()
        {
            int Id = 1;

            var result = await controller.Get(Id);
            var resultObjectResult = await controller.Get(Id) as ObjectResult;
            var resultObject = resultObjectResult.Value as TaskHouseApi.Model.Worker;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(TestWorker.Id, resultObject.Id);
        }

        public async void WorkerController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            int WorkerId = 5000;

            //Act
            var result = await controller.Get(WorkerId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);

        }

        [Fact]
        public async void WorkerController_Get_ReturnsAllElementsInRepo_WhenGivenNoParameters()
        {
            IEnumerable<Worker> result = await controller.Get();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void WorkerController_Create_ReturnsObjectResult_withValidWorker()
        {
            Worker TestWorker = new Worker()
            {
                Id = 5,
                Username = "Tusername",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen6",
                Salt = "upYKQSsrlub5JAID61/6pA=="
            };

            await controller.Create(TestWorker);
            var result = await controller.Get(TestWorker.Id);
            var resultObjectResult = await controller.Get(TestWorker.Id) as ObjectResult;
            var resultObject = resultObjectResult.Value as TaskHouseApi.Model.Worker;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(TestWorker.Id, resultObject.Id);
        }

        [Fact]
        public async void WorekrController_Create_ReturnsBadRequest_WhenGivenNullWorker()
        {
            Worker worker = null;

            var result = await controller.Create(worker);

            Assert.IsType<BadRequestObjectResult>(result);


        }

        [Fact]
        public async void WorkerController_Update_ReturnsBadRequestResult_WhenIdIsInvalidAndWorkerIsNull()
        {
            Worker worker = null;
            int id = 2600;

            var result = await controller.Update(id, worker);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void WorkerController_Update_ReturnsBadRequestResult_WhenIdIsInvalid()
        {
            Worker TestWorker = new Worker()
            {
                Id = 1,
                Username = "Tusername2",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen6",
                Salt = "upYKQSsrlub5JAID61/6pA=="
            };

            int id = 10000000;

            var result = await controller.Update(id, TestWorker);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void WorkerController_Delete_ReturnsNoContentResult_WhenIdIsValid()
        {
            int Id = 1;
            var result = await controller.Delete(Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void WorkerController_Update_ReturnsBadRequestResult_WhenWorkerIsNull()
        {
            Worker worker = null;
            int id = 1;

            var result = await controller.Update(id, worker);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void WorkerController_Delete_ActuallyDeletes_WhenIdIsValid()
        {
            Worker TestWorker = new Worker()
            {
                Id = 5,
                Username = "Tusername",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen6",
                Salt = "upYKQSsrlub5JAID61/6pA=="
            };

            await controller.Delete(TestWorker.Id);
            var result = await controller.Get(TestWorker.Id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void WorkerController_Delete_ReturnsNotFoundResult_WhenIdIsInvalid()
        {
            int id = 2500;
      
            var result = await controller.Delete(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void WorkerController_Update_ReturnsNoContentResult_withValidIdAndValidWorker()
        {
            Worker Pre_TestWorker = new Worker()
            {
                Id = 5,
                Username = "Tusername",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen6",
                Salt = "upYKQSsrlub5JAID61/6pA=="
            };

            Worker Post_TestWorker = new Worker()
            {
                Id = 5,
                Username = "Tusername2",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen6",
                Salt = "upYKQSsrlub5JAID61/6pA=="
            };

            await controller.Create(Pre_TestWorker);
            await controller.Update(Pre_TestWorker.Id, Post_TestWorker);
            var resultAsObject = await controller.Get(Post_TestWorker.Id) as ObjectResult;
            var resultObject = resultAsObject.Value as Worker;

            Assert.Equal(Post_TestWorker, resultObject);

        }
    }
}
