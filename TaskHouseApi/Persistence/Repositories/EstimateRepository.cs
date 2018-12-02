using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseApi.Persistence.Repositories
{
    public class EstimateRepository : Repository<Estimate>, IEstimateRepository
    {
        // Mappes context to postgresContext
        protected internal PostgresContext postgresContext { get { return context as PostgresContext; } }

        public EstimateRepository(PostgresContext db) : base(db) { }

        public IEnumerable<Estimate> RetrieveAllEstimatesForSpecificTaskId(int Id)
        {
            return dbSet.Where(e => e.TaskId == Id).ToList();
        }
    }
}
