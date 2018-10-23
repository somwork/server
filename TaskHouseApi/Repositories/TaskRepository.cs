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

        private static ConcurrentDictionary<int, TaskHouseApi.Model.Task> taskCache;

        private PostgresContext db;


        public TaskRepository(PostgresContext db) { 
            this.db = db;

            if (taskCache == null)
            { 
                taskCache = new ConcurrentDictionary<int, TaskHouseApi.Model.Task>(
                    db.Tasks.ToDictionary(c => c.Id));
            }
        }

        public async Task<TaskHouseApi.Model.Task> Create(TaskHouseApi.Model.Task t) { 
            await db.Tasks.AddAsync(t); 

            int affected = await db.SaveChangesAsync(); 

            if(affected != 1) { 
                return null;
            }

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