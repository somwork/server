using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using System.Linq;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseApi.Service
{
    public class AuthService : IAuthService
    {
        private IPasswordService passwordService;
        private IUnitOfWork unitOfWork;

        public AuthService(IPasswordService passwordService, IUnitOfWork unitOfWork)
        {
            this.passwordService = passwordService;
            this.unitOfWork = unitOfWork;
        }

        public User Authenticate(LoginModel loginModel)
        {
            User potentialUser = (unitOfWork.Users.RetrieveAll())
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
