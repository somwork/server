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
            var okResult = Assert.IsType<ObjectResult>(result);

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
            var okResult = Assert.IsType<BadRequestObjectResult>(result);

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
            var okResult = Assert.IsType<NoContentResult>(result);
        }
    }
}