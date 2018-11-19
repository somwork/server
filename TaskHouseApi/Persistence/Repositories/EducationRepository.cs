using TaskHouseApi.Model;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseApi.Persistence.Repositories
{
    public class EducationRepository : Repository<Education>, IEducationRepository
    {
        // Mappes context to postgresContext
        protected internal PostgresContext postgresContext { get { return context as PostgresContext; } }

        public EducationRepository(PostgresContext db) : base(db) { }
    }
}
