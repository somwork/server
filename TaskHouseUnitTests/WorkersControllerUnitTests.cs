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
        //var
        WorkersController controller;
        IWorkerRepository repo;

        //Constructor
      public WorkersControllerUnitTests()
        {
            repo = new FakeWorkerRepository();
            controller = new WorkersController(repo);
        }

       /// Get: with Id 
        [Fact]
        public async System.Threading.Tasks.Task Get_Worker_with_Id_UnitTest() 
        { 
            //Test Worker:
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


            //test:
            var result = await controller.Get(TestWorker.Id);
            var resultObjectResult = await controller.Get(TestWorker.Id) as ObjectResult;
            var resultObject = resultObjectResult.Value as TaskHouseApi.Model.Worker;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(TestWorker.Id, resultObject.Id);

        }
      
          [Fact]
        public async System.Threading.Tasks.Task Create_Worker_UnitTest() 
        { 
            //Test Worker:
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


             //test:
            await controller.Create(TestWorker);
            var result = await controller.Get(TestWorker.Id);
            var resultObjectResult = await controller.Get(TestWorker.Id) as ObjectResult;
            var resultObject = resultObjectResult.Value as TaskHouseApi.Model.Worker;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(TestWorker.Id, resultObject.Id);

        }



   
        //Delete
        [Fact]
        public async System.Threading.Tasks.Task Delete_Worker_UnitTest() 
        { 
            //Test Worker:
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


            //Test
            await controller.Delete(TestWorker.Id) ;
            var result = await controller.Get(TestWorker.Id);
          

            //Assert
            Assert.IsType<NotFoundResult>(result);
            Assert.IsType<NotFoundResult>(result);

        }

        // update worker test
        [Fact]
        public async System.Threading.Tasks.Task update_Worker_UnitTest() 
        {

            //Test Pre_Worker:
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
            //Test Post_Worker:
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


            //test:
            //Creates user
            await controller.Create(Pre_TestWorker);
            //updates user
            var Result = await controller.Update(Pre_TestWorker.Id, Post_TestWorker);

            var ResultObjectResult = await controller.Get(Post_TestWorker.Id) as ObjectResult;
            var ResultObject = ResultObjectResult.Value as TaskHouseApi.Model.Worker;

            //Assert
            Assert.IsType<ActionResult<Worker>>(Result);
            Assert.Equal(Post_TestWorker.Username, ResultObject.Username);

        }

    }
}
