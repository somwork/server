using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<Task> GetAcceptedTasksForEmployer(int Id)
        {
            return dbSet
                .Include(task => task.Estimates)
                .Where(task => task.EmployerId == Id && task.Estimates.Any(w => w.Accepted && task.Id == w.TaskId))
                .ToList();
        }

        public IEnumerable<Task> GetAcceptedTasksForWorker(int Id)
        {
            return dbSet
                .Include(task => task.Estimates)
                .Where(task => task.Estimates.Any(w => w.WorkerId == Id && w.Accepted && task.Id == w.TaskId))
                .ToList();
        }

        public IEnumerable<Task> GetEstimatedTasksForWorker(int Id)
        {
            return dbSet
                .Include(task => task.Estimates)
                .Where(task => task.Estimates.Any(w => w.WorkerId == Id && w.Accepted == false && task.Id == w.TaskId))
                .ToList();
        }

        //get all available tasks for workers.
        public IEnumerable<Task> GetAvailableTasksForWorker()
        {
            double temp = 0;

            return dbSet
                .Include(estimate => estimate.Estimates)
                .Where(task => task.AverageEstimate == temp || task.Estimates.Any(e => e.Accepted == false))
                .ToList();
        }



    }
}
