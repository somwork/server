using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;


namespace TaskHouseApi.Repositories 
{ 
    public class SkillRepository : ISkillRepository { 

        //cache the skills in a thread-safe dictionary to improve performanc
        private static ConcurrentDictionary<int, Skill> skillCache;

        //reference to database context
        private PostgresContext db;


        public SkillRepository(PostgresContext db) { 
            this.db = db;

            //populates skillCache
            if (skillCache == null)
            { 
                skillCache = new ConcurrentDictionary<int, Skill>(
                    db.Skills.ToDictionary(c => c.Id));
            }
        }

        public async Task<Skill> Create(Skill s) { 
           
            //add skill to database using ef core
            await db.Skills.AddAsync(s); 

            int affected = await db.SaveChangesAsync(); 
            
            //skill not created
            if(affected != 1) { 
                return null;
            }

            //if the skill is new, add it to the cache, else call updatecache method
            return skillCache.AddOrUpdate(s.Id, s, UpdateCache);
        }

        public async Task<IEnumerable<Skill>> RetrieveAll() 
        { 
            return await System.Threading.Tasks.Task.Run<IEnumerable<Skill>>( 
                () => skillCache.Values);
        }

        public async Task<Skill> Retrieve(int id) 
        { 
           return await System.Threading.Tasks.Task.Run(() => 
           { 
               Skill skill; 
               skillCache.TryGetValue(id, out skill);
               return skill;
           });
        }


        public async Task<Skill> Update(int Id, Skill s) 
        { 
            return await System.Threading.Tasks.Task.Run(() => 
            {
                db.Skills.Update(s);
                int affected = db.SaveChanges();

                if(affected != 1)
                { 
                    return null;
                }

                return System.Threading.Tasks.Task.Run(() => UpdateCache(Id, s));
            });
        }

        public async Task<bool> Delete(int Id) 
        { 
            return await System.Threading.Tasks.Task.Run(() => 
            { 
               Skill s = db.Skills.Find(Id);
               db.Skills.Remove(s);
               int affected = db.SaveChanges();
               
               if (affected != 1) 
               { 
                   return null;
               } 
               return System.Threading.Tasks.Task.Run(() => skillCache.TryRemove(Id, out s));
            });
        }

        private Skill UpdateCache(int Id, Skill s) 
        { 
            Skill old; 
            if (skillCache.TryGetValue(Id, out old))
            { 
                if (skillCache.TryUpdate(Id, s, old))
                { 
                    return s;
                }
            }
            return null;
        }

    }

}