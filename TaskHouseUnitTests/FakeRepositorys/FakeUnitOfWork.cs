using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.UnitOfWork;

namespace TaskHouseUnitTests.FakeRepositorys
{
    public class FakeUnitOfWork : IUnitOfWork
    {
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

        public void Dispose()
        {
            // NOOT NEEDED IN TEST
        }

        public int Save()
        {
            // NOOT NEEDED IN TEST
            return 0;
        }
    }
}
