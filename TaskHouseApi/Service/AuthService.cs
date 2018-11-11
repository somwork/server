using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using TaskHouseApi.Repositories;
using System.Linq;
using TaskHouseApi.DatabaseContext;

namespace TaskHouseApi.Service
{
    public class AuthService : IAuthService
    {
        private IPasswordService passwordService;
        private IUserRepository userRepository;

        public AuthService(IPasswordService passwordService, IUserRepository userRepository)
        {
            this.passwordService = passwordService;
            this.userRepository = userRepository;
        }

        public User Authenticate(LoginModel loginModel)
        {
            User potentialUser = (userRepository.RetrieveAll())
                .SingleOrDefault(user => user.Username.Equals(loginModel.Username));

            if (potentialUser == null || !isPasswordCorrect(loginModel, potentialUser))
            {
                return null;
            }

            return potentialUser;
        }

        private bool isPasswordCorrect(LoginModel loginModel, User potentialUser)
        {
            return passwordService
                .GenerateSaltedHashedPassword(loginModel.Password, potentialUser.Salt)
                .Equals(potentialUser.Password);
        }
    }
}
