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
using TaskHouseUnitTests.FakeRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

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

        private EmployersController createContext(EmployersController con)
        {
            con.ControllerContext = new ControllerContext();
            //Creates a new HttpContext
            con.ControllerContext.HttpContext = new DefaultHttpContext();

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
            CreateUserModel<Employer> cm = new CreateUserModel<Employer>()
            {
                User = employer,
                Password = null
            };

            //Act
            var result = controller.Create(cm);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// Test Post with valid Employer
        [Fact]
        public void EmpolyeresController_Create_ReturnsObjectResultContainingCreatedEmpolyer_WhenGivenValidEmpolyer()
        {
            controller = createContext(controller);
            //Arrange
            Employer employer = new Employer()
            {
                Id = 7,
                Username = "7777",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob1",
                LastName = "Bobsen1",
                Salt = "upYKQSsrlub5JAID61/6pA==",
                Discriminator = "Employer"
            };
            CreateUserModel<Employer> cm = new CreateUserModel<Employer>()
            {
                User = employer,
                Password = employer.Password
            };

            //Act
            var result = controller.Create(cm);
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

        [Fact]
        public void EmployersController_Update_ReturnsVoidUpdatePart_withValidIdAndValidEmployer()
        {
            int Id = 1;
            Employer update = new Employer()
            {
                Id = 10,
                Username = "Tusernamedasdasg",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=111", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = null
            };
            Employer updatedEmployer = new Employer()
            {
                Id = 1,
                Username = "Tusernamedasdasg",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen1",
                Salt = "upYKQSsrlub5JAID61/6pA==",
                Discriminator = "Employer"

            };

            var result = controller.Update(Id, update);
            var resultAsObject = controller.Get(Id) as ObjectResult;
            var resultObject = resultAsObject.Value as Employer;

            Assert.Equal(updatedEmployer.Username, resultObject.Username);
            Assert.NotEqual(update.Password, resultObject.Password);
            Assert.Equal(updatedEmployer.FirstName, resultObject.FirstName);
            Assert.NotNull(resultObject.LastName);
            Assert.NotNull(resultObject.Salt);
        }

        [Fact]
        public void EmployersController_GetTasksForEmployer_ReturnsObjectResult_WhenGivenIdForWorkerWithTasks()
        {
            int Id = 1;
            var result = controller.GetTasksForEmployer(Id);
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Task>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(3, resultObject.Count());
        }

        [Fact]
        public void EmployersController_GetTasksForEmployer_ReturnsObjectResult_WhenGivenIdForWorkerWithNoTasks()
        {
            int Id = 3;
            var result = controller.GetTasksForEmployer(Id);
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Task>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(0, resultObject.Count());
        }
    }
}
