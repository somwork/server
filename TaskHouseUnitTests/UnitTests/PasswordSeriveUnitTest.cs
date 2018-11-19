using TaskHouseApi.Service;
using Xunit;

namespace TaskHouseUnitTests.UnitTests
{
    public class PasswordServiceUnitTest
    {
        private IPasswordService passwordService;

        public PasswordServiceUnitTest()
        {
            passwordService = new PasswordService();
        }

        [Fact]
        public void PasswordService_GenerateSaltedHashedPassword_ReturnCorrectSaltAndHashedPassword_WhenGivenVaildInput()
        {
            string password = "1234";
            string salt = "upYKQSsrlub5JAID61/6pA==";
            string correctSaltedAndHashedAndBase64Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=";

            var result = passwordService.GenerateSaltedHashedPassword(password, salt);

            Assert.Equal(correctSaltedAndHashedAndBase64Password, result);
        }

        [Fact]
        public void PasswordService_GenerateSaltedHashedPassword_ReturnANotMatchingSaltAndHashedPassword_WhenGivenInvaildPassword()
        {
            string password = "12345";
            string salt = "upYKQSsrlub5JAID61/6pA==";
            string correctSaltedAndHashedAndBase64Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=";

            var result = passwordService.GenerateSaltedHashedPassword(password, salt);

            Assert.NotEqual(correctSaltedAndHashedAndBase64Password, result);
        }

        [Fact]
        public void PasswordService_GenerateSaltedHashedPassword_ReturnANotMatchingSaltAndHashedPassword_WhenGivenInvaildSalt()
        {
            string password = "1234";
            string salt = "TupYKQSsrlub5JAID61/6pA==";
            string correctSaltedAndHashedAndBase64Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=";

            var result = passwordService.GenerateSaltedHashedPassword(password, salt);

            Assert.NotEqual(correctSaltedAndHashedAndBase64Password, result);
        }

        [Fact]
        public void PasswordService_GenerateSaltedHashedPassword_ReturnANotMatchingSaltAndHashedPassword_WhenGivenInvaildSaltAndPassword()
        {
            string password = "41234";
            string salt = "TupYKQSsrlub5JAID61/6pA==";
            string correctSaltedAndHashedAndBase64Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=";

            var result = passwordService.GenerateSaltedHashedPassword(password, salt);

            Assert.NotEqual(correctSaltedAndHashedAndBase64Password, result);
        }
    }
}
