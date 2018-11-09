using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface IEmployerRepository
    {
        Employer Create(Employer e);

        IEnumerable<Employer> RetrieveAll();

        Employer Retrieve(int Id);

        Employer Update(Employer e);

        bool Delete(int Id);
    }
}
