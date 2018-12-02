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
        private ILocationRepository locationRepository;
        private ISkillRepository skillRepository;
        private ITaskRepository taskRepository;
        private IEstimateRepository estimateRepository;
        private IReferenceRepository referenceRepository;
        private IEducationRepository educationRepository;
        private ICategoryRepository categoryRepository;
        private ICurrencyRepository currencyRepository;
        private IMessageRepository messageRepository;
        private IBudgetRepository budgetRepository;

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
        public IEstimateRepository Estimates
        {
            get
            {
                if (this.estimateRepository == null)
                {
                    this.estimateRepository = new FakeEstimateRepository();
                }
                return estimateRepository;
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
        public ICategoryRepository Categorys
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

        public IBudgetRepository Budgets
        {
            get
            {
                if (this.budgetRepository == null)
                {
                    this.budgetRepository = new FakeBudgetRepository();
                }
                return budgetRepository;
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
    }
}
