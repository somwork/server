using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaskHouseApi.Service;

namespace TaskHouseUnitTests
{
    public class UsersControllerUnitTests
    {
        UsersController controller;
        IUserRepository repo;
        private IPasswordService passwordService;

        public UsersControllerUnitTests()
        {
            repo = new FakeUserRepository();
            controller = new UsersController(repo, passwordService);
        }

        [Fact]
        public async void Get()
        {

            IEnumerable<User> result = await controller.Get("");


            Assert.Equal(3, result.Count());
            
        }
    }
}
