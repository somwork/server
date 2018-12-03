using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaskHouseApi.Service;
using TaskHouseUnitTests.FakeRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace TaskHouseUnitTests.UnitTests
{
    public class QualityAssurancesControllerUnitTests
    {
        IUnitOfWork unitOfWork;
        QualityAssurancesController controller;
        IPasswordService passwordService = new PasswordService();

        public QualityAssurancesControllerUnitTests()
        {
            unitOfWork = new FakeUnitOfWork();
            controller = new QualityAssurancesController(unitOfWork, passwordService);
        }

        private QualityAssurancesController createContext(QualityAssurancesController con)
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

        [Fact]
        public void QualityAssurancesController_Get_ReturnsObjectResult_WhenGivenValidId()
        {
            int Id = 9;
            var result = controller.Get(Id);
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as QualityAssurance;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(Id, resultObject.Id);
        }

        [Fact]
        public void QualityAssurancesController_Get_ReturnsNotFound_WhenGivenInvalidId()
        {
            int QualityAssuranceId = 5000;

            var result = controller.Get(QualityAssuranceId) as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void QualityAssurancesController_Get_ReturnsAllElementsInRepo_WhenGivenNoParameters()
        {
            var result = controller.Get();
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as IEnumerable<User>;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(3, resultObject.Count());
        }

        [Fact]
        public void QualityAssurancesController_Create_ReturnsObjectResult_withValidQualityAssurance()
        {
            controller = createContext(controller);
            QualityAssurance TestQualityAssurance = new QualityAssurance()
            {
                Id = 9,
                Username = "Tusername",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=",//1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen6",
                Salt = "upYKQSsrlub5JAID61/6pA=="
            };

            var result = controller.Create(TestQualityAssurance.Password, TestQualityAssurance);
            var resultObjectResult = result as ObjectResult;
            var resultObject = resultObjectResult.Value as QualityAssurance;

            Assert.IsType<ObjectResult>(result);
            Assert.Equal(TestQualityAssurance.Id, resultObject.Id);
        }

        [Fact]
        public void QualityAssurancesController_Create_ReturnsBadRequest_WhenGivenNullQualityAssurance()
        {
            QualityAssurance QualityAssurance = null;

            var result = controller.Create(null, QualityAssurance);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void QualityAssurancesController_Delete_ReturnsNoContentResult_WhenIdIsValid()
        {
            int Id = 7;
            var result = controller.Delete(Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void QualityAssurancesController_Update_ReturnsBadRequestResult_WhenQualityAssuranceIsNull()
        {
            QualityAssurance QualityAssurance = null;

            var result = controller.Update(0, QualityAssurance);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void QualityAssurancesController_Delete_ActuallyDeletes_WhenIdIsValid()
        {
            var Id = 7;

            controller.Delete(Id);
            var result = controller.Get(Id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void QualityAssurancesController_Delete_ReturnsNotFoundResult_WhenIdIsInvalid()
        {
            int id = 2500;

            var result = controller.Delete(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void QualityAssurancesController_Update_ReturnsObjectResult_withValidIdAndValidQualityAssurance()
        {
            int Id = 8;
            QualityAssurance QualityAssurance = new QualityAssurance()
            {
                Id = 7,
                Username = "QA",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob1",
                LastName = "Bobsen1",
                Salt = "upYKQSsrlub5JAID61/6pA==",
                Discriminator = "QualityAssurance"
            };

            var Result = controller.Update(Id, QualityAssurance);
            var resultAsObject = controller.Get(Id) as ObjectResult;
            var resultObject = resultAsObject.Value as QualityAssurance;

            Assert.IsType<NoContentResult>(Result);
            Assert.Equal(QualityAssurance.Username, resultObject.Username);
        }

        [Fact]
        public void QualityAssurancesController_Update_ReturnsVoidUpdatePart_withValidIdAndValidQualityAssurance()
        {
            int Id = 7;
            QualityAssurance update = new QualityAssurance()
            {
                Id = 10,
                Username = "Tusernamedasdasg",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=111", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = null
            };
            QualityAssurance updatedQualityAssurance = new QualityAssurance()
            {
                Id = 7,
                Username = "Tusernamedasdasg",
                Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                Email = "test@test.com",
                FirstName = "Bob7",
                LastName = "Bobsen1",
                Salt = "upYKQSsrlub5JAID61/6pA==",
                Discriminator = "QualityAssurance"
            };

            var result = controller.Update(Id, update);
            var resultAsObject = controller.Get(Id) as ObjectResult;
            var resultObject = resultAsObject.Value as QualityAssurance;

            Assert.Equal(updatedQualityAssurance.Username, resultObject.Username);
            Assert.NotEqual(update.Password, resultObject.Password);
            Assert.Equal(updatedQualityAssurance.FirstName, resultObject.FirstName);
            Assert.NotNull(resultObject.LastName);
            Assert.NotNull(resultObject.Salt);
        }
    }
}
