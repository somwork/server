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
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        // Mappes context to postgresContext
        protected internal PostgresContext postgresContext { get { return context as PostgresContext; } }

        public TaskRepository(PostgresContext db) : base(db) { }

        public IEnumerable<Task> GetTasksForEmployer(int Id)
        {
            return dbSet.Where(e => e.EmployerId == Id).ToList();
        }
    }
}
