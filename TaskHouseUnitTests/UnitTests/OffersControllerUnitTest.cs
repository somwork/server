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
    public class OffersControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        OffersController controller;

        public OffersControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new OffersController(unitOfWork);
        }

        private OffersController createContext(OffersController con)
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
        public void OffersController_Get_ReturnAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Offer>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(3, resultObject.Count());
        }

        [Fact]
        //Test GET with id
        public void OffersController_Get_ReturnObject_WhenIdIsValid()
        {
            //Arrange id for offer object
            int offerId = 1;

            //Act
            var result = controller.Get(offerId);
            var resultAsObject = controller.Get(offerId) as ObjectResult;
            var resultObject = resultAsObject.Value as Offer;

            //Assert - Checks if the returned object is the same type and then checks id
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(offerId, resultObject.Id);
        }

        [Fact]
        // Test GET with invalid ID
        public void OfferController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            //Arrange id for location object
            int offerId = 403;

            //Act
            var result = controller.Get(offerId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        //Test POST for creating new offer
        public void OfferController_Create_ReturnsObject_WhenNewObject()
        {
            controller = createContext(controller);
            //Arrange new ObjectResult
            var offer = new Offer()
            {
                Id = 4,
                Accepted = false,
                Price = 213.2M,
                Currency = "DKK",
                WorkerId = 4,
                TaskId = 1
            };

            //Act
            var result = controller.Create(offer);
            var resultAsObject = result as ObjectResult;
            var resultObject = resultAsObject.Value as Offer;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(offer.Accepted, resultObject.Accepted);
        }

        [Fact]
        //Test POST for creating new offer that is null
        public void OfferController_Create_ReturnsBadRequest_WhenObjectIsNull()
        {
            //Arrange new ObjectResult
            Offer offer = null;

            //Act
            var result = controller.Create(offer);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        // Test PUT for update offer
        public void OfferController_Update_ReturnsNoContentResultAndCreatedbject_WhenParametersAreValid()
        {
            //Arrange
            Offer offer = new Offer()
            {
                Id = 1,
                Accepted = false,
                Price = 213.2M,
                Currency = "DKK",
                WorkerId = 4,
                TaskId = 1
            };
            int id = 1;

            //Act
            var result = controller.Update(id, offer);
            var resultAsObject = controller.Get(offer.Id) as ObjectResult;
            var resultObject = resultAsObject.Value as Offer;

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(offer.Accepted, resultObject.Accepted);
        }

        ///Test put with invalid Id and null Offer object
        [Fact]
        public void OfferController_Update_ReturnsBadRequestResult_WhenOfferIsNull()
        {
            //Arrange
            Offer offer = null;
            int id = 0;

            //Act
            var result = controller.Update(id, offer);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        // Test PUT for update Offer when Id is invalid
        public void OfferController_Update_ReturnsBadRequest_WhenIdIsInvalid()
        {
            //Arrange
            Offer offer = new Offer()
            {
                Id = 1,
                Accepted = false,
                Price = 213.2M,
                Currency = "DKK",
                WorkerId = 4,
                TaskId = 1
            };
            int id = 100;

            //Act
            var result = controller.Update(id, offer);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        // Test DELETE for Offer
        public void OfferController_Delete_ReturnsNoContentResult_WhenDeleted()
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
        //Test DELETE for invalid Id for Offer
        public void OfferController_Delete_ReturnsNotFoundResult_WhenIdInvalid()
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
