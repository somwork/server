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
    public class MessagesControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        MessagesController controller;

        public MessagesControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new MessagesController(unitOfWork);
        }

        private MessagesController createContext(MessagesController con)
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
        public void MessagesController_Get_ReturnAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<Message>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(3, resultObject.Count());
        }

        [Fact]
        //Test GET with id
        public void MessagesController_Get_ReturnObject_WhenIdIsValid()
        {
            //Arrange id for message object
            int messageId = 1;

            //Act
            var result = controller.Get(messageId);
            var resultAsObject = controller.Get(messageId) as ObjectResult;
            var resultObject = resultAsObject.Value as Message;

            //Assert - Checks if the returned object is the same type and then checks id
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(messageId, resultObject.Id);
        }

        [Fact]
        // Test GET with invalid ID
        public void MessagesController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            //Arrange id for location object
            int messageId = 403;

            //Act
            var result = controller.Get(messageId) as NotFoundResult;

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        //Test POST for creating new message
        public void MessagesController_Create_ReturnsObject_WhenNewObject()
        {
            controller = createContext(controller);
            //Arrange new ObjectResult
            var message = new Message()
            {
                Id = 4,
                Text = "text",
                SendAt = new DateTime(2018, 3, 3),
                UserId = 1,
                TaskId = 1
            };

            //Act
            var result = controller.Create(message);
            var resultAsObject = result as ObjectResult;
            var resultObject = resultAsObject.Value as Message;

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(message.Text, resultObject.Text);
        }

        [Fact]
        //Test POST for creating new message that is null
        public void MessagesController_Create_ReturnsBadRequest_WhenObjectIsNull()
        {
            //Arrange new ObjectResult
            Message message = null;

            //Act
            var result = controller.Create(message);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        // Test PUT for update message
        public void MessagesController_Update_ReturnsNoContentResultAndCreatedbject_WhenParametersAreValid()
        {
            //Arrange
            Message message = new Message()
            {
                Id = 1,
                Text = "text",
                SendAt = new DateTime(2018, 3, 3),
                UserId = 1,
                TaskId = 1
            };
            int id = 1;

            //Act
            var result = controller.Update(id, message);
            var resultAsObject = controller.Get(message.Id) as ObjectResult;
            var resultObject = resultAsObject.Value as Message;

            //Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(message.Text, resultObject.Text);
        }

        ///Test put with invalid Id and null message object
        [Fact]
        public void MessagesController_Update_ReturnsBadRequestResult_WhenMessageIsNull()
        {
            //Arrange
            Message message = null;
            int id = 0;

            //Act
            var result = controller.Update(id, message);

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        // Test PUT for update message when Id is invalid
        public void MessagesController_Update_ReturnsBadRequest_WhenIdIsInvalid()
        {
            //Arrange
            Message message = new Message()
            {
                Id = 4,
                Text = "text",
                SendAt = new DateTime(2018, 3, 3),
                UserId = 1,
                TaskId = 1
            };
            int id = 100;

            //Act
            var result = controller.Update(id, message);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        // Test DELETE for message
        public void MessagesController_Delete_ReturnsNoContentResult_WhenDeleted()
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
        //Test DELETE for invalid Id for message
        public void MessagesController_Delete_ReturnsNotFoundResult_WhenIdInvalid()
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
