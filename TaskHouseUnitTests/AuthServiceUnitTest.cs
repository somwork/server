using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using TaskHouseApi.Repositories;
using TaskHouseApi.Service;
using Xunit;

namespace TaskHouseUnitTests
{
    public class AuthServiceUnitTest
    {
        private IPasswordService passwordService;
        private IUserRepository userRepository;
        private IAuthService authService;

        public AuthServiceUnitTest()
        {
            passwordService = new PasswordService();
            userRepository = new FakeUserRepository();
            authService = new AuthService(passwordService, userRepository);
        }

        [Fact]
        public void AuthService_Authenticate_WhenGivenVaildLoginModelEmployer()
        {
            LoginModel lm = new LoginModel { Username = "1234", Password = "1234" };

            var result = authService.Authenticate(lm);

            Assert.IsType<Employer>(result);
            Assert.Equal(result.Username, lm.Username);
        }

        [Fact]
        public void AuthService_Authenticate_WhenGivenVaildLoginModelWorker()
        {
            LoginModel lm = new LoginModel { Username = "w1", Password = "1234" };

            var result = authService.Authenticate(lm);

            Assert.IsType<Worker>(result);
            Assert.Equal(result.Username, lm.Username);
        }

        [Fact]
        public void AuthService_Authenticate_WhenGivenInvalidUsernameInLoginModel()
        {
            LoginModel lm = new LoginModel { Username = "----", Password = "1234" };

            var result = authService.Authenticate(lm);

            Assert.Null(result);
        }

        [Fact]
        public void AuthService_Authenticate_WhenGivenInvalidPasswordInLoginModel()
        {
            LoginModel lm = new LoginModel { Username = "1234", Password = "----" };

            var result = authService.Authenticate(lm);

            Assert.Null(result);
        }

        [Fact]
        public void AuthService_Authenticate_WhenGivenInvalidPasswordAndUsernameInLoginModel()
        {
            LoginModel lm = new LoginModel { Username = "----", Password = "----" };

            var result = authService.Authenticate(lm);

            Assert.Null(result);
        }
    }
}
