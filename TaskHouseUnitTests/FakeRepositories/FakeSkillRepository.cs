using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeSkillRepository : FakeRepository<Skill>, ISkillRepository
    {
        public FakeSkillRepository()
            : base(new List<Skill>()
            {
                new Skill()
                {
                    Id = 1,
                    Title = "Skill1"
                },
                new Skill()
                {
                    Id = 2,
                    Title = "Skill2"
                },
                new Skill()
                {
                    Id = 3,
                    Title = "Skill3"
                }
            }
            )
        { }

        public IEnumerable<Skill> GetSkillsForWorker(int Id)
        {
            return list.Where(w => w.WorkerId == Id).ToList();
        }
    }
}

