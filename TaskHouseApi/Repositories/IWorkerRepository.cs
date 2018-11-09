using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface IWorkerRepository
    {
        Worker Create(Worker w);

        IEnumerable<Worker> RetrieveAll();

        Worker Retrieve(int Id);

        Worker Update(Worker w);

        bool Delete(int Id);
    }
}
