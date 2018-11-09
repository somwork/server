using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using TaskHouseApi.Repositories;
using System.Linq;
using TaskHouseApi.DatabaseContext;

namespace TaskHouseApi.Service
{
    public class AuthService : IAuthService
    {
        private IWorkerRepository workerRepository;
        private IEmployerRepository employerRepository;
        private IPasswordService passwordService;
        private PostgresContext db;

        public AuthService(IPasswordService passwordService, IWorkerRepository workerRepository, IEmployerRepository employerRepository, PostgresContext db)
        {
            this.workerRepository = workerRepository;
            this.employerRepository = employerRepository;
            this.passwordService = passwordService;
            this.db = db;
        }

        public bool DeleteRefrechToken(RefreshToken refreshToken)
        {
            db.Remove(refreshToken);
            db.SaveChanges();
            return true;
        }

        public User Retrieve(int Id)
        {
            Worker potentialWorker = workerRepository.Retrieve(Id);
            if (potentialWorker != null)
            {
                return potentialWorker;
            }

            Employer potentialEmployer = employerRepository.Retrieve(Id);
            if (potentialEmployer != null)
            {
                return potentialEmployer;
            }

            return null;
        }

        public User Update(User user)
        {
            if (user is Worker)
            {
                return workerRepository.Update((Worker)user);
            }

            if (user is Employer)
            {
                return employerRepository.Update((Employer)user);
            }

            return null;
        }

        public User Authenticate(LoginModel loginModel)
        {
            Worker potentialWorker = isWorker(loginModel);

            if (potentialWorker != null && isAuthenticated(loginModel, potentialWorker))
            {
                return potentialWorker;
            }

            Employer potentialEmployer = isEmployer(loginModel);

            if (potentialEmployer != null && isAuthenticated(loginModel, potentialEmployer))
            {
                return potentialEmployer;
            }

            return null;
        }

        private bool isAuthenticated(LoginModel loginModel, User potentialUser)
        {
            bool isPasswordGood = passwordService
                .GenerateSaltedHashedPassword(loginModel.Password, potentialUser.Salt)
                .Equals(potentialUser.Password);

            if (!isPasswordGood)
            {
                return false;
            }

            return true;
        }

        private Worker isWorker(LoginModel loginModel)
        {
            return (workerRepository.RetrieveAll())
                .SingleOrDefault(user => user.Username.Equals(loginModel.Username));
        }

        private Employer isEmployer(LoginModel loginModel)
        {
            return (employerRepository.RetrieveAll())
                .SingleOrDefault(user => user.Username.Equals(loginModel.Username));
        }
    }
}
