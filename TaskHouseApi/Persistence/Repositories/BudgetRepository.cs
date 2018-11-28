using TaskHouseApi.Model;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseApi.Persistence.Repositories
{
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        // Mappes context to postgresContext
        protected internal PostgresContext postgresContext { get { return context as PostgresContext; } }

        public BudgetRepository(PostgresContext db) : base(db) { }
    }
}
