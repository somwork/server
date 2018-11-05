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
    public class EmpolyerControllerUnitTests
    {
        EmployerrsController controller;
        IEmployerRepository repo;

        public EmpolyerControllerUnitTests()
        {
            repo = new  FakeEmppolyerRepository();
            controller = new EmployerrsController(repo);
        }
        
        /// Test Get all
        [Fact]
        public async void EmpolyerController_Get_ReturnsAllElementsInRepo_WhenGivenNoParameters()
        {
            //Arrange and act
            IEnumerable<Employer> result = await controller.Get;

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
            var assertResult = Assert.IsType<ObjectResult>(result);
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

        /// Test Post with valid Skill 
        [Fact]
        public async void SkillsController_Create_ReturnsObjectResultContainingCreatedSkill_WhenGivenValidSkill()
        { 
            //Arrange
            Employer employer = new Employer();
            .Title = "TestSkill";

            //Act
            var result = await controller.Create(skill);
            var createdResultObject = result as ObjectResult;
            var createdSkill = createdResultObject.Value as Skill;

            //Assert
            var assertResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(createdSkill.Title, skill.Title);
            Assert.Equal(createdSkill.Id, skill.Id);

        }

        ///Test post with null Skill object
        [Fact]
         public async void SkillsController_Create_ReturnsBadRequest_WhenGivenNullSkill()
        { 
            //Arrange
            Skill skill = null;

            //Act
            var result = await controller.Create(skill);

            //Assert
            var assertResult = Assert.IsType<BadRequestObjectResult>(result);
            //Major inconsistencies in whether is return BadRequestResult or BadRequestObjectResult

        }

        ///Test put with valid Id and Skill object
        [Fact]
        public async void SkillsController_Update_ReturnsNoContentResult_WhenParametersAreValid()
        { 
            //Arrange
            Skill skill = new Skill(){
                Id = 1, 
                Title = "UpdatedSkill"
            };
            int id = 1;

            //Act
            var result = await controller.Update(id, skill);
            var updatedResultObject = await controller.Get(id) as ObjectResult;
            var updatedSkill = updatedResultObject.Value as Skill;

            //Assert
            var assertResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(updatedSkill.Title, skill.Title);
        }

        ///Test put with invalid Id and valid Skill object
        [Fact]
        public async void SkillsController_Update_ReturnsBadRequestResult_WhenIdIsInvalid()
        { 
            //Arrange
            Skill skill = new Skill(){
                Id = 1, 
                Title = "Skill1"
            };
            int id = 50;

            //Act
            var result = await controller.Update(id, skill);

            //Assert
            var assertResult = Assert.IsType<BadRequestResult>(result); 
            //inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        ///Test put with valid Id and null Skill object
        [Fact]
        public async void SkillsController_Update_ReturnsBadRequestResult_WhenSkillIsNull()
        { 
            //Arrange
            Skill skill = null;
            int id = 1;

            //Act
            var result = await controller.Update(id, skill);

            //Assert
            var assertResult = Assert.IsType<BadRequestResult>(result); 
            //inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        ///Test put with invalid Id and null Skill object
        [Fact]
        public async void SkillsController_Update_ReturnsBadRequestResult_WhenIdIsInvalidAndSkillIsNull()
        { 
            //Arrange
            Skill skill = null;
            int id = 2600;

            //Act
            var result = await controller.Update(id, skill);

            //Assert
            var assertResult = Assert.IsType<BadRequestResult>(result); 
            //Inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        ///Test put with valid Id and Skill object, on not existing skill
        [Fact]
        public async void SkillsController_Update_ReturnsNotFoundResult_WhenParametersAreValidButSkillDoesNotExist()
        { 
            //Arrange
            Skill skill = new Skill(){
                Id = 10, 
                Title = "Skill1"
            };
            int id = 10;

            //Act
            var result = await controller.Update(id, skill);

            //Assert
            var assertResult = Assert.IsType<NotFoundResult>(result);
        }


        ///Test Delete returns NoContentResult with valid Id
        [Fact] 
        public async void SkillsController_Delete_ReturnsNoContentResult_WhenIdIsValid()
        { 
            //Arrange
            int id = 1; 

            //Act
            var result = await controller.Delete(id);

            //Assert
            var assertResult = Assert.IsType<NoContentResult>(result);
        }

         ///Test if delete actually deletes with valid Id
        [Fact] 
        public async void SkillsController_Delete_ActuallyDeletes_WhenIdIsValid()
        { 
            //Arrange
            int id = 1; 

            //Act
            var result = await controller.Delete(id);
            var getDeletedSkillResult = await controller.Get(id);

            //Assert
            var assertResult = Assert.IsType<NoContentResult>(result);
            var assertDeleteResult = Assert.IsType<NotFoundResult>(getDeletedSkillResult);
            
        }

        ///Test Delete with invalid Id
        [Fact] 
        public async void SkillsController_Delete_ReturnsNotFoundResult_WhenIdIsInvalid()
        { 
            //Arrange
            int id = 2500; 

            //Act
            var result = await controller.Delete(id);

            //Assert
            var assertResult = Assert.IsType<NotFoundResult>(result);
        }

    }
}