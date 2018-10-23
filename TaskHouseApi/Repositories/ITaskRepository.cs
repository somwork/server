using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface ITaskRepository 
    { 
        Task<TaskHouseApi.Model.Task> Create(TaskHouseApi.Model.Task t); 

        Task<IEnumerable<TaskHouseApi.Model.Task>> RetrieveAll();

        Task<TaskHouseApi.Model.Task> Retrieve(int id);

        Task<TaskHouseApi.Model.Task> Update (int id, TaskHouseApi.Model.Task t);

        Task<bool> Delete(int id);




    }
}