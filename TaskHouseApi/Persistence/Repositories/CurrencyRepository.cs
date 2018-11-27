using System;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseApi.Persistence.Repositories
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        // Mappes context to postgresContext
        protected internal PostgresContext postgresContext { get { return context as PostgresContext; } }

        public CurrencyRepository(PostgresContext db) : base(db) { }
    }
}
