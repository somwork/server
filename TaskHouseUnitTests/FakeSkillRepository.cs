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

            //add skill to database using ef core
            //await db.Skills.AddAsync(s);

            //int affected = await db.SaveChangesAsync();

            //skill not created
            //if (affected != 1)
            //{
            //    return null;
            //}

            //if the skill is new, add it to the cache, else call updatecache method
            skillCache.Add(s);
            return s;
            //return skillCache.AddOrUpdate(s.Id, s, UpdateCache);
        }

        public async Task<IEnumerable<Skill>> RetrieveAll()
        {
            //return await System.Threading.Tasks.Task.Run<IEnumerable<Skill>>(
            //    () => skillCache.Values);
            return skillCache;
        }

        public async Task<Skill> Retrieve(int id)
        {
            //return await System.Threading.Tasks.Task.Run(() =>
            //{
            //    Skill skill;
            //    skillCache.TryGetValue(id, out skill);
            //    return skill;
            // });

            return skillCache.Where(s => s.Id == id).SingleOrDefault();
        }


        public async Task<Skill> Update(int Id, Skill s)
        {
            Skill old = skillCache.Where(skill => skill.Id == Id).SingleOrDefault();
            int index = skillCache.IndexOf(old);

            skillCache[index] = s;
            return s;
           // return await System.Threading.Tasks.Task.Run(() =>
            //{
            //    db.Skills.Update(s);
            //    int affected = db.SaveChanges();

            //    if (affected != 1)
            //    {
            //        return null;
            //    }

            //    return System.Threading.Tasks.Task.Run(() => UpdateCache(Id, s));
           // });
        }

        public async Task<bool> Delete(int Id)
        {
            Skill skill = skillCache.Where(s => s.Id == Id).SingleOrDefault();

            if(skill == null) return false;

            skillCache.Remove(skill);

            return true;
            //return await System.Threading.Tasks.Task.Run(() =>
            //{
            //    Skill s = db.Skills.Find(Id);
            //    db.Skills.Remove(s);
            //    int affected = db.SaveChanges();

            //    if (affected != 1)
            //    {
            //        return null;
            //    }
            //    return System.Threading.Tasks.Task.Run(() => skillCache.TryRemove(Id, out s));
           // });
        }

    }
}