using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface IWorkerRepository
    {
        System.Threading.Tasks.Task Create(Worker w);

        Task<IEnumerable<Worker>> RetrieveAll();

        Task<Worker> Retrieve(int Id);

        Task<Worker> Update(int Id, Worker w);
    

        Task<bool> Delete(int Id);
    }
}
