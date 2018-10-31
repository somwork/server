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
    public class SkillsControllerUnitTests 
    { 
        SkillsController controller; 
        ISkillRepository repo; 

        public SkillsControllerUnitTests() 
        { 
            repo = new FakeSkillRepository();
            controller = new SkillsController(repo);
        }

        ///Test Get all
        [Fact]
        public async void  SkillsController_ReturnsAllElementsInRepo() 
        { 
            //Arrange and act
            IEnumerable<Skill> result = await controller.Get();

            //Asserts
            Assert.Equal(3, result.Count());

        }

        ///Test Get with valid Id as parameter
        [Fact]
        public async void SkillsController_Get_ReturnsObjectReponse_WhenGivenValidId() 
        { 
            //arrange
            int skillId = 2;

            //Act
            var result = await controller.Get(skillId);

            //Assert
            var okResult = Assert.IsType<ObjectResult>(result);

        }

        ///Test Get with invalid Id as parameter
        [Fact]
        public  async void SkillsController_Get_ReturnsNotFound_WhenGivenInvalidId()
        { 
            //arrange
            int skillId = 5000;

            //Act
            var result = await controller.Get(skillId) as NotFoundResult;
            
            //Assert
            Assert.Equal(404, result.StatusCode);

        }

        /// Test Post with valid Skill 
        [Fact]
        public async void SkillsController_Create_ReturnsObjectResult_WhenGivenValidSkill()
        { 
            //Arrange
            Skill skill = new Skill();
            skill.Title = "TestSkill";

            //Act
            var result = await controller.Create(skill);

            //Assert
            var assertResult = Assert.IsType<ObjectResult>(result);

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
                Title = "Skill1"
            };
            int id = 1;

            //Act
            var result = await controller.Update(id, skill);

            //Assert
            var assertResult = Assert.IsType<NoContentResult>(result);
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


        ///Test Delete with valid Id
        [Fact] 
        public async void SkillsController_Delete_ReturnsNoContentResult_WhenIdIsValid()
        { 
            //Arrange
            int id = 1; 

            //Act
            var result = await controller.Delete(id);

            //Assert
            var asserrResult = Assert.IsType<NoContentResult>(result);
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
            var asserrResult = Assert.IsType<NotFoundResult>(result);
        }
        

    }
}