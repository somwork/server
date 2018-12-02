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
using TaskHouseUnitTests.FakeRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace TaskHouseUnitTests.UnitTests
{
    public class UsersControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        UsersController controller;

        public UsersControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new UsersController(unitOfWork);
        }

        [Fact]
        public void UsersController_Get_ReturnsObjectResult_WhenGivenValidId()
        {
            int Id = 4;
            var result = controller.Get(Id);
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as User;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(Id, resultObject.Id);
        }

        [Fact]
        public void UsersController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            int UserId = 5000;

            var result = controller.Get(UserId) as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }
    }
}
