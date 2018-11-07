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
        public async void Get_Worker_with_Id()
        {
            Worker TestWorker =  new Worker()
            {
                Id = 1,
                Username = "1234",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob1",
                LastName = "Bobsen1",
                Salt = "upYKQSsrlub5JAID61/6pA=="
            };

            var result = await controller.Get(TestWorker.Id);
            var resultObjectResult = await controller.Get(TestWorker.Id) as ObjectResult;
            var resultObject = resultObjectResult.Value as TaskHouseApi.Model.Worker;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(TestWorker.Id, resultObject.Id);
        }

        [Fact]
        public async void Get_Worker_All()
        {
            repo = new FakeWorkerRepository();

            IEnumerable<Worker> result = await controller.Get();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void Create_Worker()
        {
            Worker TestWorker =  new Worker()
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
        public async void Delete_Worker()
        {
            Worker TestWorker =  new Worker()
            {
                Id = 5,
                Username = "Tusername",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen6",
                Salt = "upYKQSsrlub5JAID61/6pA=="
            };

            await controller.Delete(TestWorker.Id) ;
            var result = await controller.Get(TestWorker.Id);

            Assert.IsType<NotFoundResult>(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void update_Worker()
        {
            Worker Pre_TestWorker =  new Worker()
            {
                Id = 5,
                Username = "Tusername",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen6",
                Salt = "upYKQSsrlub5JAID61/6pA=="
            };

            Worker Post_TestWorker =  new Worker()
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

            var Result = await controller.Update(Pre_TestWorker.Id, Post_TestWorker);

            var ResultObjectResult = await controller.Get(Post_TestWorker.Id) as ObjectResult;
            var ResultObject = ResultObjectResult.Value as TaskHouseApi.Model.Worker;

            Assert.IsType<ActionResult<Worker>>(Result);
            Assert.Equal(Post_TestWorker.Username, ResultObject.Username);
        }
    }
}
