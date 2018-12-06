using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;

namespace TaskHouseApi.Persistence.Repositories.Interfaces
{
    public interface ITaskRepository : IRepository<Task>
    {
        IEnumerable<Task> GetTasksForEmployer(int Id);
        IEnumerable<Task> GetAcceptedTasksForEmployer(int Id);
        IEnumerable<Task> GetAcceptedTasksForWorker(int Id);
        IEnumerable<Task> GetEstimatedTasksForWorker(int Id);
        IEnumerable<Task> GetAvailableTasksForWorker();
        bool AddCategory(int taskId, int categoryId);
    }
}
