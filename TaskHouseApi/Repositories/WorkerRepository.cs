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
    public class WorkerRepository : IWorkerRepository
    {
        //cache the Workers in a thread-safe dictionary to improve performance
        private static ConcurrentDictionary<int, Worker> WorkerCache;

        //reference to database context
        private PostgresContext db;

        public WorkerRepository(PostgresContext db)
        {
            this.db = db;

            //populates WorkerCache
            if (WorkerCache == null)
            {
                WorkerCache = new ConcurrentDictionary<int, Worker>(
                    db.Workers.ToDictionary(c => c.Id));
            }
        }

        public async Task<Worker> Create(Worker w)
        {
            //add Worker to database using EF core
            EntityEntry<Worker> added = await db.Workers.AddAsync(w);

            int affected = await db.SaveChangesAsync();

            //Worker not created
            if (affected != 1)
            {
                return null;
            }

            // if the customer is new, add it to cache, else call UpdateCache method
            return WorkerCache.AddOrUpdate(w.Id, w, UpdateCache);
        }

        public async Task<IEnumerable<Worker>> RetrieveAll()
        {
            return await System.Threading.Tasks.Task.Run<IEnumerable<Worker>>(
                () => WorkerCache.Values);
        }

        public async Task<Worker> RetrieveSpecific(LoginModel loginModel)
        {
            return (await RetrieveAll())
                .Single(Worker => Worker.Username == loginModel.Username && Worker.Password == loginModel.Password);
        }

        public async Task<Worker> Retrieve(int Id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                Worker w;
                WorkerCache.TryGetValue(Id, out w);
                return w;
            });
        }

        public async Task<Worker> Update(int Id, Worker w)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                db.Workers.Update(w);
                int affected = db.SaveChanges();

                if (affected != 1)
                {
                    return null;
                }

                return System.Threading.Tasks.Task.Run(() => UpdateCache(Id, w));
            });
        }

        public async Task<bool> Delete(int Id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                Worker w = db.Workers.Find(Id);
                db.Workers.Remove(w);
                int affected = db.SaveChanges();

                if (affected != 1)
                {
                    return null;
                }
                return System.Threading.Tasks.Task.Run(() => WorkerCache.TryRemove(Id, out w));
            });
        }

        private Worker UpdateCache(int Id, Worker w)
        {
            Worker old;
            if (WorkerCache.TryGetValue(Id, out old))
            {
                if (WorkerCache.TryUpdate(Id, w, old))
                {
                    return w;
                }
            }
            return null;
        }

      
    }
}
