using System.Collections.Generic;
using TaskHouseApi.Model;

namespace TaskHouseApi.Persistence.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesForTask(int taskId);
    }
}
