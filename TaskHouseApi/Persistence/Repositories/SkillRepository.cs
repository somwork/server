using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseApi.Persistence.Repositories
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        // Mappes context to postgresContext
        protected internal PostgresContext postgresContext { get { return context as PostgresContext; } }

        public SkillRepository(PostgresContext db) : base(db) { }

        public IEnumerable<Skill> GetSkillsForWorker(int Id)
        {
            return dbSet.Where(w => w.WorkerId == Id).ToList();
        }
    }
}
