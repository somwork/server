using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        private IUserRepository<User> userRepository;
        private IWorkerRepository workerRepository;
        private IEmployerRepository employerRepository;
        private IQualityAssuranceRepository qualityAssuranceRepository;
        private ILocationRepository locationRepository;
        private ISkillRepository skillRepository;
        private ITaskRepository taskRepository;
        private IOfferRepository offerRepository;
        private IReferenceRepository referenceRepository;
        private IEducationRepository educationRepository;
        private ICategoryRepository categoryRepository;
        private ICurrencyRepository currencyRepository;
        private IMessageRepository messageRepository;

        public IUserRepository<User> Users
        {
            get
            {
                if (this.userRepository == null)
                {
                    List<User> w = (Workers.RetrieveAll()).Select(m => (User)m).ToList();
                    List<User> e = (Employers.RetrieveAll()).Select(m => (User)m).ToList();
                    w.AddRange(e);

                    this.userRepository = new FakeUserRepository<User>(w);
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
                    this.workerRepository = new FakeWorkerRepository();
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
                    this.employerRepository = new FakeEmployerRepository();
                }
                return employerRepository;
            }
        }
        public IQualityAssuranceRepository QualityAssurances
        {
            get
            {
                if (this.qualityAssuranceRepository == null)
                {
                    this.qualityAssuranceRepository = new FakeQualityAssuranceRepository();
                }
                return qualityAssuranceRepository;
            }
        }
        public ILocationRepository Locations
        {
            get
            {
                if (this.locationRepository == null)
                {
                    this.locationRepository = new FakeLocationRepository();
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
                    this.skillRepository = new FakeSkillRepository();
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
                    this.taskRepository = new FakeTaskRepository();
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
                    this.offerRepository = new FakeOfferRepository();
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
                    this.referenceRepository = new FakeReferenceRepository();
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
                    this.educationRepository = new FakeEducationRepository();
                }
                return educationRepository;
            }
        }
        public ICategoryRepository Categories
        {
            get
            {

                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new FakeCategoryRepository();
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
                    this.currencyRepository = new FakeCurrencyRepository();
                }
                return currencyRepository;
            }
        }

        public IMessageRepository Messages
        {
            get
            {

                if (this.messageRepository == null)
                {
                    this.messageRepository = new FakeMessageRepository();
                }
                return messageRepository;
            }
        }

        public void Dispose()
        {
            // NOT NEEDED IN TEST
        }

        public int Save()
        {
            // NOT NEEDED IN TEST
            return 0;
        }

        public IRepository<T> Repository<T>() where T : BaseModel
        {
            var basemodelType = typeof(T);

            if (basemodelType == typeof(User)) { return ((IRepository<T>)Users); }
            if (basemodelType == typeof(Worker)) { return ((IRepository<T>)Workers); }
            if (basemodelType == typeof(Employer)) { return ((IRepository<T>)Employers); }
            if (basemodelType == typeof(QualityAssurance)) { return ((IRepository<T>)QualityAssurances); }
            if (basemodelType == typeof(Location)) { return ((IRepository<T>)Locations); }
            if (basemodelType == typeof(Skill)) { return ((IRepository<T>)Skills); }
            if (basemodelType == typeof(Task)) { return ((IRepository<T>)Tasks); }
            if (basemodelType == typeof(Offer)) { return ((IRepository<T>)Offers); }
            if (basemodelType == typeof(Reference)) { return ((IRepository<T>)References); }
            if (basemodelType == typeof(Education)) { return ((IRepository<T>)Educations); }
            if (basemodelType == typeof(Category)) { return ((IRepository<T>)Categories); }
            if (basemodelType == typeof(Currency)) { return ((IRepository<T>)Currencies); }
            if (basemodelType == typeof(Message)) { return ((IRepository<T>)Messages); }

            return null;
        }
    }
}
