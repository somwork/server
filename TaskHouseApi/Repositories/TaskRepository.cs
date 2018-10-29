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
    public class TaskRepository : ITaskRepository { 

        //cache the tasks in a thread-safe dictionary to improve performanc
        private static ConcurrentDictionary<int, TaskHouseApi.Model.Task> taskCache;

        //reference to database context
        private PostgresContext db;


        public TaskRepository(PostgresContext db) { 
            this.db = db;

            //populates taskCache
            if (taskCache == null)
            { 
                taskCache = new ConcurrentDictionary<int, TaskHouseApi.Model.Task>(
                    db.Tasks.ToDictionary(c => c.Id));
            }
        }

        public async Task<TaskHouseApi.Model.Task> Create(TaskHouseApi.Model.Task t) { 
           
            //add task to database using ef core
            await db.Tasks.AddAsync(t); 

            int affected = await db.SaveChangesAsync(); 
            
            //task not created
            if(affected != 1) { 
                return null;
            }

            //if the task is new, add it to the cache, else call updatecache method
            return taskCache.AddOrUpdate(t.Id, t, UpdateCache);
        }

        public async Task<IEnumerable<TaskHouseApi.Model.Task>> RetrieveAll() 
        { 
            return await System.Threading.Tasks.Task.Run<IEnumerable<TaskHouseApi.Model.Task>>( 
                () => taskCache.Values);
        }

        public async Task<TaskHouseApi.Model.Task> Retrieve(int id) 
        { 
           return await System.Threading.Tasks.Task.Run(() => 
           { 
               TaskHouseApi.Model.Task task; 
               taskCache.TryGetValue(id, out task);
               return task;
           });
        }


        public async Task<TaskHouseApi.Model.Task> Update(int Id, TaskHouseApi.Model.Task t) 
        { 
            return await System.Threading.Tasks.Task.Run(() => 
            {
                db.Tasks.Update(t);
                int affected = db.SaveChanges();

                if(affected != 1)
                { 
                    return null;
                }

                return System.Threading.Tasks.Task.Run(() => UpdateCache(Id, t));
            });
        }

        public async Task<bool> Delete(int Id) 
        { 
            return await System.Threading.Tasks.Task.Run(() => 
            { 
               TaskHouseApi.Model.Task t = db.Tasks.Find(Id);
               db.Tasks.Remove(t);
               int affected = db.SaveChanges();
               
               if (affected != 1) 
               { 
                   return null;
               } 
               return System.Threading.Tasks.Task.Run(() => taskCache.TryRemove(Id, out t));
            });
        }

        private TaskHouseApi.Model.Task UpdateCache(int Id, TaskHouseApi.Model.Task t) 
        { 
            TaskHouseApi.Model.Task old; 
            if (taskCache.TryGetValue(Id, out old))
            { 
                if (taskCache.TryUpdate(Id, t, old))
                { 
                    return t;
                }
            }
            return null;
        }

    }

}