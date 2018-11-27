using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskHouseApi.Model;
using System.Linq;
using System;
using TaskHouseUnitTests.FakeRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace TaskHouseUnitTests.UnitTests
{
    public class EducationsControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        EducationsController controller;

        public EducationsControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new EducationsController(unitOfWork);
        }

        private EducationsController createContext(EducationsController con)
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

        //Test retrieve all in repository
        [Fact]
        public void EducationsController_Get_ReturnAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Education>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(3, resultObject.Count());
        }

        [Fact]
        //Test GET with id
        public void EducationsController_Get_ReturnObject_WhenIdIsValid()
        {
            //Arrange id for education object
            int educationId = 1;

            //Act
            var result = controller.Get(educationId);
            var resultAsObject = controller.Get(educationId) as ObjectResult;
            var resultObject = resultAsObject.Value as Education;

            //Assert - Checks if the returned object is the same type and then checks id
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(educationId, resultObject.Id);
        }

        [Fact]
        // Test GET with invalid ID
        public void EducationsController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            //Arrange id for location object
            int educationId = 403;

            //Act
            var result = controller.Get(educationId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        //Test POST for creating new education
        public void EducationsController_Create_ReturnsObject_WhenNewObject()
        {
            controller = createContext(controller);
            //Arrange new ObjectResult
            var education = new Education()
            {
                Id = 1,
                Title = "edu1",
                Start = new DateTime(2018, 3, 3),
                End = new DateTime(2019, 3, 3),
                WorkerId = 4
            };

            //Act
            var result = controller.Create(education);
            var resultAsObject = result as ObjectResult;
            var resultObject = resultAsObject.Value as Education;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(education.Title, resultObject.Title);
        }

        [Fact]
        //Test POST for creating new education that is null
        public void EducationsController_Create_ReturnsBadRequest_WhenObjectIsNull()
        {
            //Arrange new ObjectResult
            Education education = null;

            //Act
            var result = controller.Create(education);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        // Test PUT for update education
        public void EducationsController_Update_ReturnsNoContentResultAndCreatedbject_WhenParametersAreValid()
        {
            //Arrange
            Education education = new Education()
            {
                Id = 1,
                Title = "edu1",
                Start = new DateTime(2018, 3, 3),
                End = new DateTime(2019, 3, 3),
                WorkerId = 4
            };
            int id = 1;

            //Act
            var result = controller.Update(id, education);
            var resultAsObject = controller.Get(education.Id) as ObjectResult;
            var resultObject = resultAsObject.Value as Education;

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(education.Title, resultObject.Title);
        }

        ///Test put with invalid Id and null education object
        [Fact]
        public void EducationsController_Update_ReturnsBadRequestResult_WhenEducationIsNull()
        {
            //Arrange
            Education education = null;
            int id = 0;

            //Act
            var result = controller.Update(id, education);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        // Test PUT for update education when Id is invalid
        public void EducationsController_Update_ReturnsBadRequest_WhenIdIsInvalid()
        {
            //Arrange
            Education education = new Education()
            {
                Id = 1,
                Title = "edu1",
                Start = new DateTime(2018, 3, 3),
                End = new DateTime(2019, 3, 3),
                WorkerId = 4
            };
            int id = 100;

            //Act
            var result = controller.Update(id, education);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        // Test DELETE for education
        public void EducationsController_Delete_ReturnsNoContentResult_WhenDeleted()
        {
            //Arrange
            int Id = 1;

            //Act
            var result = controller.Delete(Id);
            var getDeletedResult = controller.Get(Id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.IsType<NotFoundResult>(getDeletedResult);
        }

        [Fact]
        //Test DELETE for invalid Id for education
        public void EducationsController_Delete_ReturnsNotFoundResult_WhenIdInvalid()
        {
            //Arrange
            int Id = 100;

            //Act
            var result = controller.Delete(Id);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }
    }
}
