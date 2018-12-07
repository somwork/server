using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseApi.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        // Mappes context to postgresContext
        protected internal PostgresContext postgresContext { get { return context as PostgresContext; } }

        public CategoryRepository(PostgresContext db) : base(db) { }

        public IEnumerable<Category> GetCategoriesForTask(int taskId)
        {
            return dbSet
                .Include(c => c.CategoryTask)
                .Where(c => c.CategoryTask.Any(t => t.TaskId == taskId))
                .ToList();
        }
    }
}
