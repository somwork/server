using TaskHouseApi.Model;
using TaskHouseApi.Repositories;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskHouseUnitTests
{
    public class FakeSkillRepository : ISkillRepository
    {
        private static List<Skill> skillCache;

        public FakeSkillRepository()
        {
            skillCache = new List<Skill>()
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
                },

            };
        }

        public async Task<Skill> Create(Skill s)
        {
            skillCache.Add(s);
            return s;
        }

        public async Task<IEnumerable<Skill>> RetrieveAll()
        {
            return skillCache;
        }

        public async Task<Skill> Retrieve(int id)
        {
            return skillCache.Where(s => s.Id == id).SingleOrDefault();
        }


        public async Task<Skill> Update(int Id, Skill s)
        {
            Skill old = skillCache.Where(skill => skill.Id == Id).SingleOrDefault();
            int index = skillCache.IndexOf(old);

            skillCache[index] = s;
            return s;

        }

        public async Task<bool> Delete(int Id)
        {
            Skill skill = skillCache.Where(s => s.Id == Id).SingleOrDefault();

            if (skill == null) return false;

            skillCache.Remove(skill);

            return true;

        }

    }
}