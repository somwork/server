using TaskHouseApi.Model;
using TaskHouseApi.Repositories;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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

        public Skill Create(Skill s)
        {
            skillCache.Add(s);
            return s;
        }

        public IEnumerable<Skill> RetrieveAll()
        {
            return skillCache;
        }

        public Skill Retrieve(int id)
        {
            return skillCache.Where(s => s.Id == id).SingleOrDefault();
        }

        public Skill Update(Skill s)
        {
            Skill old = Retrieve(s.Id);
            int index = skillCache.IndexOf(old);

            skillCache[index] = s;
            return s;
        }

        public bool Delete(int Id)
        {
            Skill temp = skillCache.Find(s => s.Id == Id);
            skillCache.Remove(temp);
            temp = skillCache.Find(s => s.Id == Id);

            if (temp != null)
            {
                return false;
            }
            return true;
        }
    }
}
