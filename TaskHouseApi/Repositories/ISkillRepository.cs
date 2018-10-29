using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface ISkillRepository 
    { 
        Task<Skill> Create(Skill s); 

        Task<IEnumerable<Skill>> RetrieveAll();

        Task<Skill> Retrieve(int id);

        Task<Skill> Update (int id, Skill s);

        Task<bool> Delete(int id);

    }
}