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
        public void SkillsController_Get_ReturnsAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Skill>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(3, resultObject.Count());
        }

        ///Test Get with valid Id as parameter
        [Fact]
        public void SkillsController_Get_ReturnsObjectReponseWithCorrectSkill_WhenGivenValidId()
        {
            //arrange
            int skillId = 2;

            //Act
            var result = controller.Get(skillId);
            var resultAsObject = controller.Get(skillId) as ObjectResult;
            var resultObject = resultAsObject.Value as Skill;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(skillId, resultObject.Id);

        }

        ///Test Get with invalid Id as parameter
        [Fact]
        public void SkillsController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            //arrange
            int skillId = 5000;

            //Act
            var result = controller.Get(skillId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);

        }

        /// Test Post with valid Skill
        [Fact]
        public void SkillsController_Create_ReturnsObjectResultContainingCreatedSkill_WhenGivenValidSkill()
        {
            //Arrange
            Skill skill = new Skill();
            skill.Title = "TestSkill";

            //Act
            var result = controller.Create(skill);
            var createdResultObject = result as ObjectResult;
            var createdSkill = createdResultObject.Value as Skill;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(createdSkill.Title, skill.Title);
            Assert.Equal(createdSkill.Id, skill.Id);

        }

        ///Test post with null Skill object
        [Fact]
        public void SkillsController_Create_ReturnsBadRequest_WhenGivenNullSkill()
        {
            //Arrange
            Skill skill = null;

            //Act
            var result = controller.Create(skill);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            //Major inconsistencies in whether is return BadRequestResult or BadRequestObjectResult

        }

        ///Test put with valid Id and Skill object
        [Fact]
        public void SkillsController_Update_ReturnsNoContentResult_WhenParametersAreValid()
        {
            //Arrange
            Skill skill = new Skill()
            {
                Id = 1,
                Title = "UpdatedSkill"
            };

            //Act
            var result = controller.Update(skill);
            var updatedResultObject = controller.Get(skill.Id) as ObjectResult;
            var updatedSkill = updatedResultObject.Value as Skill;

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(updatedSkill.Title, skill.Title);
        }

        ///Test put with valid Id and null Skill object
        [Fact]
        public void SkillsController_Update_ReturnsBadRequestResult_WhenSkillIsNull()
        {
            //Arrange
            Skill skill = null;

            //Act
            var result = controller.Update(skill);

            //Assert
            Assert.IsType<BadRequestResult>(result);
            //inconsistencies across tests in whether it returns BadRequestResult or BadRequestObjectResult
        }

        ///Test put with valid Id and Skill object, on not existing skill
        [Fact]
        public void SkillsController_Update_ReturnsNotFoundResult_WhenParametersAreValidButSkillDoesNotExist()
        {
            //Arrange
            Skill skill = new Skill()
            {
                Id = 10,
                Title = "Skill1"
            };

            //Act
            var result = controller.Update(skill);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        ///Test Delete returns NoContentResult with valid Id
        [Fact]
        public void SkillsController_Delete_ReturnsNoContentResult_WhenIdIsValid()
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
        public void SkillsController_Delete_ActuallyDeletes_WhenIdIsValid()
        {
            //Arrange
            int id = 1;

            //Act
            var result = controller.Delete(id);
            var getDeletedSkillResult = controller.Get(id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.IsType<NotFoundResult>(getDeletedSkillResult);

        }

        ///Test Delete with invalid Id
        [Fact]
        public void SkillsController_Delete_ReturnsNotFoundResult_WhenIdIsInvalid()
        {
            //Arrange
            int id = 2500;

            //Act
            var result = controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
