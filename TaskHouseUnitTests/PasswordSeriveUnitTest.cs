using TaskHouseApi.Service;
using Xunit;

namespace TaskHouseUnitTests
{
    public class PasswordServiceUnitTest
    {
        private IPasswordService passwordService;

        public PasswordServiceUnitTest()
        {
            passwordService = new PasswordService();
        }

        [Fact]
        public void PasswordService_GenerateSaltedHashedPassword_WhenGivenVaildInput()
        {
            string password = "1234";
            string salt = "upYKQSsrlub5JAID61/6pA==";

            var result = passwordService.GenerateSaltedHashedPassword(password, salt);

            Assert.Equal("+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", result);
        }

        [Fact]
        public void PasswordService_GenerateSaltedHashedPassword_WhenGivenInvaildPassword()
        {
            string password = "12345";
            string salt = "upYKQSsrlub5JAID61/6pA==";

            var result = passwordService.GenerateSaltedHashedPassword(password, salt);

            Assert.NotEqual("+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", result);
        }

        [Fact]
        public void PasswordService_GenerateSaltedHashedPassword_WhenGivenInvaildSalt()
        {
            string password = "1234";
            string salt = "TupYKQSsrlub5JAID61/6pA==";

            var result = passwordService.GenerateSaltedHashedPassword(password, salt);

            Assert.NotEqual("+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", result);
        }

        [Fact]
        public void PasswordService_GenerateSaltedHashedPassword_WhenGivenInvaildSaltAndPassword()
        {
            string password = "41234";
            string salt = "TupYKQSsrlub5JAID61/6pA==";

            var result = passwordService.GenerateSaltedHashedPassword(password, salt);

            Assert.NotEqual("+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", result);
        }
    }
}
