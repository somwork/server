using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface ISkillRepository
    {
        Skill Create(Skill s);

        IEnumerable<Skill> RetrieveAll();

        Skill Retrieve(int id);

        Skill Update(Skill s);

        bool Delete(int id);
    }
}
