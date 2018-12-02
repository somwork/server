using System;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Model;

namespace TaskHouseApi.Persistence.Repositories.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        IEnumerable<Skill> GetSkillsForWorker(int Id);
    }
}
