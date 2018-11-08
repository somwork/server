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
    public class EmpolyersControllerUnitTests
    {
        EmployersController controller;
        IEmployersRepository repo;

        public EmpolyersControllerUnitTests()
        {
            repo = new  FakeEmpolyersRepository();
            controller = new EmployersController(repo);
        }
        
        /// Test Get all
        [Fact]
        public async void EmpolyerController_Get_ReturnsAllElementsInRepo_WhenGivenNoParameters()
        {
            //Arrange and act
            IEnumerable<Employer> result = await controller.Get();

            //Asserts
            Assert.Equal(3, result.Count());

        }

        ///Test Get with valid Id as parameter
        [Fact]
        public async void EmpolyerController_Get_ReturnsObjectReponseWithCorrectEmpolyer_WhenGivenValidId() 
        { 
            //arrange
            int empolyerId = 2;

            //Act
            var result = await controller.Get(empolyerId);
            var resultAsObject = await controller.Get(empolyerId) as ObjectResult;
            var resultObject = resultAsObject.Value as Employer;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(empolyerId, resultObject.Id);

        }

        ///Test Get with invalid Id as parameter
        [Fact]
        public  async void EmpolyerController_Get_ReturnsNotFound_WhenGivenInvalidId()
        { 
            //arrange
            int empolyerId = 5000;

            //Act
            var result = await controller.Get(empolyerId) as NotFoundResult;
            
            //Assert
            Assert.Equal(404, result.StatusCode);

        }

        

        ///Test post with null empolyer object
        [Fact]
         public async void EmpolyerController_Create_ReturnsBadRequest_WhenGivenNullEmpolyer()
        { 
            //Arrange
            Employer employer = null;

            //Act
            var result = await controller.Create(employer);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
          

        }

      
        
        ///Test put with invalid Id and null empolyer object
        [Fact]
        public async void EmpolyerController_Update_ReturnsBadRequestResult_WhenIdIsInvalidAndEmpolyerIsNull()
        { 
            //Arrange
            Employer employer = null;
            int id = 2600;

            //Act
            var result = await controller.Update(id, employer);

            //Assert
            Assert.IsType<BadRequestResult>(result); 
            //Inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        

        ///Test Delete returns NoContentResult with valid Id
        [Fact] 
        public async void EmpolyerController_Delete_ReturnsNoContentResult_WhenIdIsValid()
        { 
            //Arrange
            int id = 1; 

            //Act
            var result = await controller.Delete(id);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

         ///Test if delete actually deletes with valid Id
        [Fact] 
        public async void EmpolyersController_Delete_ActuallyDeletes_WhenIdIsValid()
        { 
            //Arrange
            int id = 1; 

            //Act
            var result = await controller.Delete(id);
            var getDeletedEmpolyerResult = await controller.Get(id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.IsType<NotFoundResult>(getDeletedEmpolyerResult);
            
        }

        ///Test Delete with invalid Id
        [Fact] 
        public async void EmpolyerController_Delete_ReturnsNotFoundResult_WhenIdIsInvalid()
        { 
            //Arrange
            int id = 2500; 

            //Act
            var result = await controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
