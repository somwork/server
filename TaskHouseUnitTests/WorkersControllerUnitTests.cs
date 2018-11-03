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
        WorkerController controller;
        iWorkerRepository repo;

        //Constructor
      public UsersControllerUnitTests()
        {
            repo = new FakeUserRepository();
            controller = new WorkersController(repo);
        }

       /// Get: with Id 
        [Fact]
        public async void Get_Worker_with_Id() 
        { 
            //Test user:
           Worker testWorker =  new User() 
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
            Worker result = await ((ObjectResult) controller.Get(testWorker.Id));
            

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(testWorker.Id, result.Id);

        }
       
       
    // get_with_username
    // Get_with_id
    //Create
    // put_with_id (update)
    //Delete



    }
}