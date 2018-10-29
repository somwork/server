using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface IWorkerRepository
    {
        Task<Worker> Create(Worker u);

        Task<IEnumerable<Worker>> RetrieveAll();

        Task<Worker> Retrieve(int Id);

        Task<Worker> RetrieveSpecific(LoginModel loginModel);

        Task<Worker> Update(int Id, Worker u);

        Task<bool> Delete(int Id);
    }
}
