using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaskHouseUnitTests.FakeRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.Security.Claims;

namespace TaskHouseUnitTests.UnitTests
{
    public class SkillsControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        SkillsController controller;

        public SkillsControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new SkillsController(unitOfWork);
        }

        private SkillsController createContext(SkillsController con)
        {
            con.ControllerContext = new ControllerContext();
            //Creates a new HttpContext
            con.ControllerContext.HttpContext = new DefaultHttpContext();

            //Adds a User with claim to the current context
            con.ControllerContext.HttpContext.User.AddIdentity(new ClaimsIdentity(new List<Claim>() {
                //Adds a claim for nameIdentifier, user Id
                new Claim(ClaimTypes.NameIdentifier, "4"),
                //Adds a claim for role, user role/tupe
                new Claim(ClaimTypes.Role, "TaskHouseApi.Model.Worker")
            }));

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
            controller = createContext(controller);
            //Arrange
            Skill skill = new Skill()
            {
                Id = 1,
                Title = "Skill1"
            };

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
            int id = 1;

            //Act
            var result = controller.Update(id, skill);
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
            int id = 0;

            //Act
            var result = controller.Update(id, skill);

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
            int id = 10;

            //Act
            var result = controller.Update(id, skill);

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
