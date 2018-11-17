using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaskHouseApi.Service;
using TaskHouseUnitTests.FakeRepositorys;

namespace TaskHouseUnitTests.UnitTests
{
    public class EmpolyersControllerUnitTests
    {
        EmployersController controller;
        IPasswordService passwordService = new PasswordService();
        IUnitOfWork unitOfWork;

        public EmpolyersControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new EmployersController(unitOfWork, passwordService);
        }

        /// Test Get all
        [Fact]
        public void EmpolyerController_Get_ReturnsAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<User>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(3, resultObject.Count());
        }

        ///Test Get with valid Id as parameter
        [Fact]
        public void EmpolyerController_Get_ReturnsObjectReponseWithCorrectEmpolyer_WhenGivenValidId()
        {
            //arrange
            int empolyerId = 2;

            //Act
            var result = controller.Get(empolyerId);
            var resultAsObject = controller.Get(empolyerId) as ObjectResult;
            var resultObject = resultAsObject.Value as Employer;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(empolyerId, resultObject.Id);

        }

        ///Test Get with invalid Id as parameter
        [Fact]
        public void EmpolyerController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            //arrange
            int empolyerId = 5000;

            //Act
            var result = controller.Get(empolyerId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);

        }



        ///Test post with null empolyer object
        [Fact]
        public void EmpolyerController_Create_ReturnsBadRequest_WhenGivenNullEmpolyer()
        {
            //Arrange
            Employer employer = null;

            //Act
            var result = controller.Create(employer);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// Test Post with valid Employer
        [Fact]
        public void EmpolyeresController_Create_ReturnsObjectResultContainingCreatedEmpolyer_WhenGivenValidEmpolyer()
        {
            //Arrange
            Employer employer = new Employer();
            employer.LastName = "TestEmpolyer";

            //Act
            var result = controller.Create(employer);
            var createdResultObject = result as ObjectResult;
            var createdEmployer = createdResultObject.Value as Employer;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(createdEmployer.LastName, employer.LastName);
            Assert.Equal(employer.Id, employer.Id);

        }

        ///Test Delete returns NoContentResult with valid Id
        [Fact]
        public void EmpolyerController_Delete_ReturnsNoContentResult_WhenIdIsValid()
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
        public void EmpolyersController_Delete_ActuallyDeletes_WhenIdIsValid()
        {
            //Arrange
            int id = 1;

            //Act
            var result = controller.Delete(id);
            var getDeletedEmpolyerResult = controller.Get(id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.IsType<NotFoundResult>(getDeletedEmpolyerResult);

        }


        ///Test Delete with invalid Id
        [Fact]
        public void EmpolyerController_Delete_ReturnsNotFoundResult_WhenIdIsInvalid()
        {
            //Arrange
            int id = 2500;

            //Act
            var result = controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        ///Test put with valid Id and Skill object
        [Fact]
        public void EmpolyersController_Update_ReturnsNoContentResult_WhenParametersAreValid()
        {
            //Arrange
            Employer employer = new Employer()
            {
                Id = 1,
                Email = "UpdatedEmail"
            };
            int id = 1;

            //Act
            var result = controller.Update(id, employer);
            var updatedResultObject = controller.Get(id) as ObjectResult;
            var updatedEmployer = updatedResultObject.Value as Employer;

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(updatedEmployer.Email, employer.Email);
        }

        ///Test put with valid Id and Skill object, on not existing skill
        [Fact]
        public void EmployersController_Update_ReturnsNotFoundResult_WhenParametersAreValidButEmployerDoesNotExist()
        {
            //Arrange
            Employer Employer = new Employer()
            {
                Id = 10,
                Email = "Email"
            };
            int id = 10;

            //Act
            var result = controller.Update(id, Employer);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        ///Test put with invalid Id and null empolyer object
        [Fact]
        public void EmpolyerController_Update_ReturnsBadRequestResult_WhenEmpolyerIsNull()
        {
            //Arrange
            Employer employer = null;
            int id = 0;

            //Act
            var result = controller.Update(id, employer);

            //Assert
            Assert.IsType<BadRequestResult>(result);
            //Inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }
    }
}
