using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.Repositories;
using TaskHouseApi.Model;

namespace TaskHouseApi.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresContext context;

        public UnitOfWork(PostgresContext context)
        {
            this.context = context;
        }

        private IUserRepository<User> userRepository;
        private IWorkerRepository workerRepository;
        private IEmployerRepository employerRepository;
        private ILocationRepository locationRepository;
        private ISkillRepository skillRepository;
        private ITaskRepository taskRepository;
        private IOfferRepository offerRepository;
        private IReferenceRepository referenceRepository;
        private IEducationRepository educationRepository;
        private ICategoryRepository categoryRepository;
        private ICurrencyRepository currencyRepository;

        public IUserRepository<User> Users
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository<User>(context);
                }
                return userRepository;
            }
        }
        public IWorkerRepository Workers
        {
            get
            {
                if (this.workerRepository == null)
                {
                    this.workerRepository = new WorkerRepository(context);
                }
                return workerRepository;
            }
        }
        public IEmployerRepository Employers
        {
            get
            {
                if (this.employerRepository == null)
                {
                    this.employerRepository = new EmployerRepository(context);
                }
                return employerRepository;
            }
        }
        public ILocationRepository Locations
        {
            get
            {
                if (this.locationRepository == null)
                {
                    this.locationRepository = new LocationRepository(context);
                }
                return locationRepository;
            }
        }
        public ISkillRepository Skills
        {
            get
            {
                if (this.skillRepository == null)
                {
                    this.skillRepository = new SkillRepository(context);
                }
                return skillRepository;
            }
        }
        public ITaskRepository Tasks
        {
            get
            {
                if (this.taskRepository == null)
                {
                    this.taskRepository = new TaskRepository(context);
                }
                return taskRepository;
            }
        }
        public IOfferRepository Offers
        {
            get
            {
                if (this.offerRepository == null)
                {
                    this.offerRepository = new OfferRepository(context);
                }
                return offerRepository;
            }
        }
        public IReferenceRepository References
        {
            get
            {

                if (this.referenceRepository == null)
                {
                    this.referenceRepository = new ReferenceRepository(context);
                }
                return referenceRepository;
            }
        }
        public IEducationRepository Educations
        {
            get
            {

                if (this.educationRepository == null)
                {
                    this.educationRepository = new EducationRepository(context);
                }
                return educationRepository;
            }
        }
        public ICategoryRepository Categorys
        {
            get
            {

                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new CategoryRepository(context);
                }
                return categoryRepository;
            }
        }
        public ICurrencyRepository Currencies
        {
            get
            {

                if (this.currencyRepository == null)
                {
                    this.currencyRepository = new CurrencyRepository(context);
                }
                return currencyRepository;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
