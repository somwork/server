using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface IEmployersRepository
    {
        Task<Employer> Create(Employer e);

        Task<IEnumerable<Employer>> RetrieveAll();

        Task<Employer> Retrieve(int Id);

        Task<Employer> Update(int Id, Employer e);

        Task<bool> Delete(int Id);

        
    }
}
