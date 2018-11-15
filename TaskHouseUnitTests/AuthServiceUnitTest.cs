using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;
using TaskHouseApi.Service;
using Xunit;

namespace TaskHouseUnitTests
{
    public class AuthServiceUnitTest
    {
        private IPasswordService passwordService;
        private IUnitOfWork unitOfWork;
        private IAuthService authService;

        public AuthServiceUnitTest()
        {
            passwordService = new PasswordService();
            unitOfWork = new FakeUnitOfWork();
            authService = new AuthService(passwordService, unitOfWork);
        }

        [Fact]
        public void AuthService_Authenticate_ReturnEmployerAllianceWithLoginModel_WhenGivenVaildLoginModelEmployer()
        {
            LoginModel lm = new LoginModel { Username = "1234", Password = "1234" };

            var result = authService.Authenticate(lm);

            Assert.IsType<Employer>(result);
            Assert.Equal(result.Username, lm.Username);
        }

        [Fact]
        public void AuthService_Authenticate_ReturnWokerAllianceWithLoginModel_WhenGivenVaildLoginModelWorker()
        {
            LoginModel lm = new LoginModel { Username = "w1", Password = "1234" };

            var result = authService.Authenticate(lm);

            Assert.IsType<Worker>(result);
            Assert.Equal(result.Username, lm.Username);
        }

        [Fact]
        public void AuthService_Authenticate_ReturnsNull_WhenGivenInvalidUsernameInLoginModel()
        {
            LoginModel lm = new LoginModel { Username = "----", Password = "1234" };

            var result = authService.Authenticate(lm);

            Assert.Null(result);
        }

        [Fact]
        public void AuthService_Authenticate_ReturnsNull_WhenGivenInvalidPasswordInLoginModel()
        {
            LoginModel lm = new LoginModel { Username = "1234", Password = "----" };

            var result = authService.Authenticate(lm);

            Assert.Null(result);
        }

        [Fact]
        public void AuthService_Authenticate_ReturnsNull_WhenGivenInvalidPasswordAndUsernameInLoginModel()
        {
            LoginModel lm = new LoginModel { Username = "----", Password = "----" };

            var result = authService.Authenticate(lm);

            Assert.Null(result);
        }
    }
}
