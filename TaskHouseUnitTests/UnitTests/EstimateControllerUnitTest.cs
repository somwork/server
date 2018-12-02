using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskHouseApi.Model;
using System.Linq;
using TaskHouseUnitTests.FakeRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace TaskHouseUnitTests.UnitTests
{
    public class EstimateControllerUnitTest
    {
        IUnitOfWork unitOfWork;
        EstimatesController controller;

        public EstimateControllerUnitTest()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new EstimatesController(unitOfWork);
        }

        private EstimatesController createContext(EstimatesController con)
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
        public void EstimatesController_Get_ReturnAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Estimate>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(4, resultObject.Count());
        }


        [Fact]
        //Test GET with  valid id
        public void EstimatesController_Get_ReturnObject_WhenIdIsValid()
        {
            //Arrange id for offer object
            int estimateId = 1;

            //Act
            var result = controller.Get(estimateId);
            var resultAsObject = controller.Get(estimateId) as ObjectResult;
            var resultObject = resultAsObject.Value as Estimate;

            //Assert - Checks if the returned object is the same type and then checks id
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(estimateId, resultObject.Id);
        }

        [Fact]
        // Test GET with invalid ID
        public void EstimatesController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            //Arrange id for location object
            int estimateId = 403;

            //Act
            var result = controller.Get(estimateId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);
        }



        [Fact]
        // Delete test with valid params
        public void EstimatesController_Delete_ReturnsNoContentResult_WhenDeleted()
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
        ///Delete with invalid ID
        public void EstimatesController_Delete_ReturnsNotFoundResult_WhenIdInvalid()
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
