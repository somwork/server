using System;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseApi.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository<User> Users { get; }
        IWorkerRepository Workers { get; }
        IEmployerRepository Employers { get; }
        ILocationRepository Locations { get; }
        ISkillRepository Skills { get; }
        ITaskRepository Tasks { get; }
        IEstimateRepository Estimates { get; }
        IReferenceRepository References { get; }
        IEducationRepository Educations { get; }
        ICategoryRepository Categorys { get; }
        ICurrencyRepository Currencies { get; }
        IMessageRepository Messages { get; }
        IBudgetRepository Budgets { get; }

        int Save();
    }
}
