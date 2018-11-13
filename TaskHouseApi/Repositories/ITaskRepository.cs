using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface ITaskRepository
    {
        Task Create(Task t);

        IEnumerable<Task> RetrieveAll();

        Task Retrieve(int id);

        Task Update(Task t);

        bool Delete(int id);
    }
}
